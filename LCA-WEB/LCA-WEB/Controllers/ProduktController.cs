using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LCA_WEB.Models;
using System.Net;

namespace LCA_WEB.Controllers
{
    public class ProduktController : Controller
    {
        private DBProduct db = new DBProduct();
        // GET: Produkt
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product personalDetail = db.Products.Find(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // POST: PersonalDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
"Id,Name,Durability,TotalWeight,DateOfCreation,DateOfChanging,CreatedBy,ChangeBy,Typ_Id")] Product productDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productDetails);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
        "Id,Name,Durability,TotalWeight,DateOfCreation,DateOfChanging,CreatedBy,ChangeBy,Typ_Id")] Product productDetails)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productDetails);
        }
    }
}