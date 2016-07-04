using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind.Store.Model;
using Northwind.Store.Business;
using Northwind.Store.Notification;
using Northwind.Store.UI.Web.Settings;

namespace Northwind.Store.UI.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RegionController : Controller
    {
        RegionB rB = new RegionB();
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public RegionController()
        {
            rs = new RequestSettings(this);
        }

        // GET: Admin/Region
        public ActionResult Index()
        {
            return View(rB.ReadList());
        }

        // GET: Admin/Region/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = rB.Read(id.Value);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Admin/Region/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegionID,RegionDescription,RowVersion")] Region region)
        {
            if (ModelState.IsValid)
            {
                region.State = State.Added;
                rB.Create(region, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError("", nm.Messages.First().Title);
                    return View(region);
                }

                return RedirectToAction("Index");
            }

            return View(region);
        }

        // GET: Admin/Region/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = rB.Read(id.Value);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Admin/Region/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "RegionID,RegionDescription,RowVersion,ModifiedProperties")] Region region)
        public ActionResult Edit([Bind(Include = "RegionID,RegionDescription,RowVersion")] Region region)
        {
            if (ModelState.IsValid)
            {
                region.State = State.Modified;
                rB.Save(region, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError("", nm.Messages.First().Title);
                    return View(region);
                }
                return RedirectToAction("Index");
            }
            return View(region);
        }

        // GET: Admin/Region/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = rB.Read(id.Value);
            if (region == null && rs.Message == null)
            {
                return HttpNotFound();
            }

            // Determinar si viene un mensaje de un intento de eliminación previo
            if (rs.Message != null)
            {
                ViewBag.Message = rs.Message;
            }

            return View(region);
        }

        // POST: Admin/Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, byte[] rowVersion)
        {

            Region region = rB.Read(id);

            if (region != null)
            {
                region.RowVersion = rowVersion; // Se asign el RowVersion original
                region.State = State.Deleted;
                rB.Save(region, nm);
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
