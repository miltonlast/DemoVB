﻿using Northwind.Store.Model;
using Northwind.Store.Notification;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    /// <summary>
    /// Clase base la gestión del acceso a los datos. Incluye métodos básicos para el acceso a los datos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataBase<T> : IDisposable where T : class, IObjectWithState
    {
        protected NorthwindEntities db = new NorthwindEntities();

        /// <summary>
        /// Aplica todos los cambios que han sido aplicados a una entidad y a todos objetos relacionados con la misma.
        /// </summary>
        /// <param name="model">Instancia del objeto.</param>
        /// <param name="nm">Mensaje de notificación (Opcional).</param>
        public void Save(T model, NotificationMessage nm = null)
        {
            try
            {
                db.ApplyChanges<T>(model);
            }
            catch (DbUpdateConcurrencyException dce)
            {
                ManageConcurrency(dce, nm);
            }
            catch (DbEntityValidationException deve)
            {
                foreach (var err in deve.EntityValidationErrors)
                {
                    foreach (var verr in err.ValidationErrors)
                    {
                        nm.Messages.Add(new Message()
                        {
                            Level = MessageLevel.Warning,
                            Description = string.Format("La propiedad {0}. Tiene {1}.",
                            verr.PropertyName, verr.ErrorMessage)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = Notification.Messages.General.EXCEPTION;

                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    msg.Description = ex.InnerException.InnerException.Message;

                    nm.Messages.Add(msg);
                }
                else
                {
                    msg.Description = ex.InnerException.Message;
                }
                nm.Messages.Add(msg);
            }
        }

        /// <summary>
        /// Administración de la excepción de concurrencia al momento de la actualización de datos. Construye una notificación cuando se incluye el parámetro NotificationMessage.
        /// </summary>
        /// <param name="dbe"></param>
        /// <param name="nm">Si el incluye la instancia se agrega los datos relacionados con la excepción.</param>
        protected void ManageConcurrency(DbUpdateConcurrencyException dbe, NotificationMessage nm = null)
        {
            // Si no se incluye el parámetro de notificación se lanza la excepción
            if (nm == null)
            {
                throw dbe;
            }
            else
            {
                var entry = dbe.Entries.First();
                var client = (T)entry.Entity;
                var serverEntry = entry.GetDatabaseValues();

                // El problema de concurrencia se da porque los datos fueron eliminados
                if (serverEntry == null)
                {
                    MessageConcurrency mc = Messages.General.CONCURRENCY_DELETE;
                    mc.Original = client;
                    nm.Messages.Add(mc);
                }
                // El problema de concurrencia se da porque los datos fueron actualizados
                else
                {
                    var server = (T)serverEntry.ToObject();

                    MessageConcurrency mc = Messages.General.CONCURRENCY_UPDATE;
                    mc.Original = client;
                    mc.Current = server;
                    nm.Messages.Add(mc);
                }
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
