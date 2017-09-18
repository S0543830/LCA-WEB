using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LCA_WEB.Models;
using Microsoft.AspNet.Identity;
using PagedList;
namespace LCA_WEB.Controllers
{
    public class HomeController : Controller
    {

        private  DbWebLcaEntities _db = new DbWebLcaEntities();
        public ActionResult Index(string sortOn, string orderBy,
            string pSortOn, string keyword, int? page)
        {
            //if (Request.IsAuthenticated)
            {

                int recordsPerPage = 10;
                if (!page.HasValue)
                {
                    page = 1; // set initial page value
                    if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                    {
                        orderBy = "desc";
                    }
                    else
                    {
                        orderBy = "asc";
                    }
                }
                if (!string.IsNullOrWhiteSpace(sortOn) && !sortOn.Equals(pSortOn,
                        StringComparison.CurrentCultureIgnoreCase))
                {
                    orderBy = "asc";
                }

                ViewBag.OrderBy = orderBy;
                ViewBag.SortOn = sortOn;
                ViewBag.Keyword = keyword;

                var list = _db.Produkts.AsQueryable();

                switch (sortOn)
                {
                    case "Name":
                        if (orderBy.Equals("desc"))
                        {
                            list = list.OrderByDescending(p => p.Name);
                        }
                        else
                        {
                            list = list.OrderBy(p => p.Name);
                        }
                        break;
                    default:
                        list = list.OrderBy(p => p.Id);
                        break;
                }
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    list = list.Where(f => f.Name.StartsWith(keyword));
                }
                var finalList = list.ToPagedList(page.Value, recordsPerPage);
                return View(finalList);



                //int recordsPerPage = 10;
                //return View(_db.Produkts.ToList().ToPagedList(page, recordsPerPage));
            }
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            
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

        [HttpGet]
        public ActionResult Create()
        {
            
            Produkt_Typ_Rohstoff_Indikator viewModel = new Produkt_Typ_Rohstoff_Indikator
            {
                _Typ_Id = _db.ProduktTyps.ToList(),
                _LIndikator = _db.Umweltindikators.ToList(),
                _Rohstoffe = _db.Rohstoffes.ToList(),
            };
            return View(viewModel);
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed(Produkt_Typ_Rohstoff_Indikator _produktDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produktDetails._Produkt.DateOfChanging = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _produktDetails._Produkt.DateOfCreation = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _produktDetails._Produkt.CreatedBy = Request.IsAuthenticated ? User.Identity.GetUserName() : "Besucher";
                    _produktDetails._Produkt.ChangedBy = Request.IsAuthenticated ? User.Identity.GetUserName() : "Besucher";

                    //Indikator speichern
                    var indi = from a in _db.Umweltindikators where a.Id == _produktDetails._Indikator.Id select a.Id;
                    _produktDetails._Umweltindikatorwert.Umweltindikator_Id = indi.FirstOrDefault();
                    _produktDetails._Umweltindikatorwert.Wert = _produktDetails._Umweltindikatorwert.Wert;
                    _db.Umweltindikatorwerts.Add(_produktDetails._Umweltindikatorwert);
                    _db.SaveChanges();
                    var idindi = from a in _db.Umweltindikatorwerts
                        where (a.Umweltindikator_Id == indi.FirstOrDefault() ) && (a.Wert == _produktDetails._Umweltindikatorwert.Wert)
                        select a.Id;
                    //Indikator speichern

                    //Rohstoff speichern
                    //var roh = from a in _db.Rohstoffs where a.Id == _produktDetails._Rohstoff.Id select a;
                    //_produktDetails._Rohstoff = roh.FirstOrDefault();
                    _produktDetails._Rohstoff.Umweltindikator_Id = idindi.FirstOrDefault();
                    _produktDetails._Rohstoff.Rohstoff_Id = _produktDetails._Rohstoff.Id;
                    _db.Rohstoffs.Add(_produktDetails._Rohstoff);
                    _db.SaveChanges();
                    var rohid = from a in _db.Rohstoffs where (a.Rohstoff_Id == _produktDetails._Rohstoff.Id) && (a.Menge_in_t == _produktDetails._Rohstoff.Menge_in_t) select a.Id;
                    //Rohstoff speichern Ende

                    //Produkt speichern
                    var pa = from a in _db.ProduktTyps where a.Id == _produktDetails._ProduktTyp.Id select a.Id;
                    _produktDetails._Produkt.Typ_Id = pa.FirstOrDefault();
                    _produktDetails._Produkt.Rohstoff_Id = rohid.FirstOrDefault();
                    _db.Produkts.Add(_produktDetails._Produkt);
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
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt_Typ_Rohstoff_Indikator produkt = new Produkt_Typ_Rohstoff_Indikator
            {
                _Typ_Id = _db.ProduktTyps.ToList(),
                _Produkt = _db.Produkts.Find(id),
                _LIndikator = _db.Umweltindikators.ToList(),
                _Rohstoffe = _db.Rohstoffes.ToList()
            };
            if (produkt._Produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Produkt_Typ_Rohstoff_Indikator _produktDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(_produktDetails._Produkt).State = EntityState.Modified;
                    _produktDetails._Produkt.DateOfChanging = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _produktDetails._Produkt.ChangedBy = Request.IsAuthenticated ? User.Identity.GetUserName() : "Besucher";
                    //Indikator speichern
                    var indi = from a in _db.Umweltindikators where a.Id == _produktDetails._Indikator.Id select a.Id;
                    _produktDetails._Umweltindikatorwert.Umweltindikator_Id = indi.FirstOrDefault();
                    _produktDetails._Umweltindikatorwert.Wert = _produktDetails._Umweltindikatorwert.Wert;
                    _db.Umweltindikatorwerts.Add(_produktDetails._Umweltindikatorwert);
                    _db.SaveChanges();
                    var idindi = from a in _db.Umweltindikatorwerts
                        where (a.Umweltindikator_Id == indi.FirstOrDefault()) && (a.Wert == _produktDetails._Umweltindikatorwert.Wert)
                        select a.Id;
                    //Indikator speichern

                    //Rohstoff speichern
                    //var roh = from a in _db.Rohstoffs where a.Id == _produktDetails._Rohstoff.Id select a;
                    //_produktDetails._Rohstoff = roh.FirstOrDefault();
                    _produktDetails._Rohstoff.Umweltindikator_Id = idindi.FirstOrDefault();
                    _produktDetails._Rohstoff.Rohstoff_Id = _produktDetails._Rohstoff.Id;
                    _db.Rohstoffs.Add(_produktDetails._Rohstoff);
                    _db.SaveChanges();
                    var rohid = from a in _db.Rohstoffs where (a.Rohstoff_Id == _produktDetails._Rohstoff.Id) && (a.Menge_in_t == _produktDetails._Rohstoff.Menge_in_t) select a.Id;
                    //Rohstoff speichern Ende

                    //Produkt speichern
                    var pa = from a in _db.ProduktTyps where a.Id == _produktDetails._ProduktTyp.Id select a.Id;
                    _produktDetails._Produkt.Typ_Id = pa.FirstOrDefault();
                    _produktDetails._Produkt.Rohstoff_Id = rohid.FirstOrDefault();
                    _db.Produkts.Add(_produktDetails._Produkt);
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

            return View(_produktDetails);
        }




        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddIndikator(string whichView)
        {
            IndikatorViewViewModel sd = new IndikatorViewViewModel();
            sd._View = whichView;
            return View(sd);
        }

        [HttpGet]
        public ActionResult AddRohstoff(string whichView)
        {
            RohstoffViewViewModel r = new RohstoffViewViewModel();
            r._View = whichView;
            return View(r);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddRohstoff(RohstoffViewViewModel _rohstoffView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Rohstoffes.Add(_rohstoffView._Rohstoffe);
                    _db.SaveChanges();
                    return RedirectToAction(_rohstoffView._View);
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
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult DeleteRohstoff(Rohstoffe _name, string roh)
        {
            if (_name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RohstoffViewViewModel sd = new RohstoffViewViewModel();
            sd._Rohstoffe = _db.Rohstoffes.FirstOrDefault(i => i.Name == _name.Name);
            sd._View = roh;
            if (sd._Rohstoffe == null)
            {
                return HttpNotFound();
            }
            return View(sd);
        }

        [HttpPost, ActionName("DeleteRohstoff")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIndikatorConfirmed(Rohstoffe _name, string roh)
        {
            var p = from a in _db.Rohstoffes where a.Name.Contains(_name.Name) select a;
            Rohstoffe asd = p.FirstOrDefault();
            if (asd != null)
            {
                _db.Rohstoffes.Remove(asd);
            }
            _db.SaveChanges();
            return RedirectToAction(roh);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddIndikator(IndikatorViewViewModel _indiDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Umweltindikators.Add(_indiDetails._Umweltindikator);
                    _db.SaveChanges();
                    return RedirectToAction(_indiDetails._View);
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
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult DeleteIndikator(Umweltindikator _name, string indi)
        {
            if (_name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndikatorViewViewModel sd = new IndikatorViewViewModel();
            sd._Umweltindikator = _db.Umweltindikators.FirstOrDefault(i => i.Name == _name.Name);
            sd._View = indi;
            if (sd._Umweltindikator == null)
            {
                return HttpNotFound();
            }
            return View(sd);
        }

        [HttpPost, ActionName("DeleteIndikator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIndikatorConfirmed(Umweltindikator _name, string indi)
        {
            var p = from a in _db.Umweltindikators where a.Name.Contains(_name.Name) select a;
            Umweltindikator asd = p.FirstOrDefault();
            if (asd != null)
            {
                _db.Umweltindikators.Remove(asd);
            }
            _db.SaveChanges();
            return RedirectToAction(indi);
        }

        [HttpGet]
        public ActionResult DeleteProduktTyp(ProduktTyp _name, string _whichView)
        {
            if (_name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProduktTyp viewModel = new ProduktTyp();
            viewModel = _db.ProduktTyps.FirstOrDefault(i => i.Name == _name.Name);
            ProduktTypViewViewModel _deleteProduktTypView = new ProduktTypViewViewModel();
            _deleteProduktTypView._ProduktTyp = viewModel;
            _deleteProduktTypView._View = _whichView;
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(_deleteProduktTypView);
        }

        [HttpPost, ActionName("DeleteProduktTyp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTypConfirmed(ProduktTyp _name, string _whichView)
        {
            var p = from a in _db.ProduktTyps where a.Name.Contains(_name.Name) select a;
            ProduktTyp asd = p.FirstOrDefault();
            if (asd != null)
            {
                _db.ProduktTyps.Remove(asd);
            }
            _db.SaveChanges();
            return RedirectToAction(_whichView);
        }

        
        public ActionResult AddProduktTyp(string whichView)
        {
            ProduktTypViewViewModel v = new ProduktTypViewViewModel();
            v._View = whichView;
            return View(v);
        }

        [HttpPost, ActionName("AddProduktTyp")]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduktTypConfirmed(ProduktTypViewViewModel _typ)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _typ._ProduktTyp.Typ = _typ._ProduktTyp.Name;
                    _db.ProduktTyps.Add(_typ._ProduktTyp);
                    _db.SaveChanges();
                    return RedirectToAction(_typ._View);
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
            return HttpNotFound();
        }


       

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt_Typ_Rohstoff_Indikator viewModel = new Produkt_Typ_Rohstoff_Indikator
            {
                _Typ_Id = _db.ProduktTyps.ToList(),
                _LIndikator = _db.Umweltindikators.ToList(),
                _Produkt = _db.Produkts.Find(id)
        };
            if (viewModel._Produkt == null)
            {
                return HttpNotFound();
            }
            return View(viewModel._Produkt);
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

    }
}