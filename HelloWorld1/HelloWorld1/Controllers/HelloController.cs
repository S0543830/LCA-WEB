using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld1.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Hello()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}