using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Create([Bind(Include ="Id,Name")] Produkt produktDetails)
        {
            //if (ModelState.IsValid)
            //{
            //    _db.Produkts.Add(produktDetails);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(produktDetails);
        }
    }
}