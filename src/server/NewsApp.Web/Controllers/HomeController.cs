using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(int userId, string deviceId)
        {
            return View();
        }
        public ActionResult TestSendById(int userId)
        {
            return View();
        }
        public ActionResult TestSend()
        {
            return View();
        }
    }
}
