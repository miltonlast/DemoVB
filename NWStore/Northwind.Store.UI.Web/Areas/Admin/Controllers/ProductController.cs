using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Business;
using Northwind.Store.Notification;
using Northwind.Store.UI.Web.Settings;

namespace Northwind.Store.UI.Web.Areas.Admin.Controllers
{
   // [Authorize(Roles="Administrator")]
    public class ProductController : Controller
    {
        ProductB rB = new ProductB();
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;


        public ProductController()
        {
            rs = new RequestSettings(this);
        }


        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(rB.ReadList());
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = rB.Read(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Region/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.State = State.Added;
                rB.Create(product, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError("", nm.Messages.First().Title);
                    return View(product);
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product  product = rB.Read(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            //ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", product.SupplierID);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.State = State.Modified;
                rB.Save(product , nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError("", nm.Messages.First().Title);
                    return View(product);
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = rB.Read(id.Value);
            if (product == null && rs.Message == null)
            {
                return HttpNotFound();
            }

            // Determinar si viene un mensaje de un intento de eliminación previo
            if (rs.Message != null)
            {
                ViewBag.Message = rs.Message;
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, byte[] rowVersion)
        {

            Product product = rB.Read(id);

            if (product != null)
            {
                product.RowVersion = rowVersion; // Se asign el RowVersion original
                product.State = State.Deleted;
                rB.Save(product, nm);
            }
            else
            {
                nm.Messages.Add(Store.Notification.Messages.General.NO_EXISTS);
            }

            if (nm.IsNotified)
            {
                rs.Message = nm.Messages.First();
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
