using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LCA_WEB.Models;
namespace LCA_WEB.Controllers
{
    public class HomeController : Controller
    {

        private DBEntities _db = new DBEntities();
        public ActionResult Index()
        {

            return View(_db.Produkts.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Imprint()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Menge")] Produkt produktDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produktDetails.DateOfChanging = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
                    produktDetails.DateOfCreation = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _db.Produkts.Add(produktDetails);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            return View(produktDetails);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = _db.Produkts.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produkt produkt = _db.Produkts.Find(id);
            if (produkt != null)
            {
                _db.Produkts.Remove(produkt);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = _db.Produkts.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateOfCreation")] Produkt produkt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(produkt).State = EntityState.Modified;
                    produkt.DateOfChanging = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }

            return View(produkt);
        }




    }
}