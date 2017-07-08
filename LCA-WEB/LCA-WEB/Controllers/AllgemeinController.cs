using LCA_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LCA_WEB.Controllers
{
    public class AllgemeinController : Controller
    {
        private DBLCA db = new DBLCA();
        // GET: Allgemein
        public ActionResult Index()
        {
            return View(db.ProduktRohstoffUmweltindikators.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Name,Menge,Nutzungsdauer_in_Jahre,DateOfCreation,DateOfChanging,CreatedBy,ChangeBy,Typ_Id")] Product productDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productDetails);
        }

        //Lesen und anzeigen
        public ActionResult Create()
        {
            
            return View();
        }
   

        //Schreibe in die Datenbank
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
        "Produkt_Id,Rohstoff_Id,Umweltindikator_Id,Rohstoff_Menge_in_t,Umweltindikator_Menge")] ProduktRohstoffUmweltindikator productDetails)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productDetails.Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productDetails);
        }
    }
}