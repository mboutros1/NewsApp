using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsAppModel.Messaging;
using NewsAppModel.Services;

namespace NewsApp.Controllers
{
    public class ChurchController : Controller
    {
        //
        // GET: /Church/
        private ChurchService _churchService;

        public ChurchController(ChurchService churchService)
        {
            _churchService = churchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UpdateSubscriptions(UserChurchSubscriptionRequest churchSubscriptionRequest)
        {
            _churchService.UpdateSubscription(churchSubscriptionRequest);
            return Json(_churchService.GetSubscription(churchSubscriptionRequest.UserId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubscriptions(int userId)
        {
            return Json(_churchService.GetSubscription(userId), JsonRequestBehavior.AllowGet);
        }

    }
}
