using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LCA_WEB.Models;
namespace LCA_WEB.Controllers
{
    public class ProduktionsmittelController : Controller
    {
        private Produktion db = new Produktion();
        // GET: Produktionsmittel
        public ActionResult Index()
        {
            return View(db.Produktionsmittel_DeutscheBahn.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalDetails/Create
        // To protect from overposting attacks, please enable the specificproperties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
        "Id,Name,Menge,Rohstoff_Typ_1,Active")] Produktionsmittel_DeutscheBahn personalDetail)
        {
            if (ModelState.IsValid)
            {
                db.Produktionsmittel_DeutscheBahn.Add(personalDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personalDetail);
        }
    }
}