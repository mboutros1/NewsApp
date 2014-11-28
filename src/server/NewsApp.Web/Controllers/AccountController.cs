using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using NewsApp.Model;
using NewsAppModel.Extensions;
using NewsAppModel.Services;

namespace NewsApp.Controllers
{


    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepository;

        private readonly UserService _userService;

        public AccountController(UserService userService, IRepository<User> userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            return Json(new User());
        }
        public JsonResult Register(int? userId, string deviceId, string deviceType)
        {
            return Json(_userService.Register(userId.GetValueOrDefault(), deviceId, deviceType).ToViewModel(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoginFb(int? userId, string email, string name, string birthdate, long facebookId, string deviceId)
        {
            return
               Json(_userService.Login(new LoginRequest(userId.GetValueOrDefault(), email, name, birthdate, facebookId, deviceId, this.GetDeviceType())).ToViewModel());
        }

        [HttpPost]
        public JsonResult Create(string userName, string password)
        {
            return Json(new User());
        }

        [HttpPost]
        public JsonResult GetUserInfo(User user)
        {
            return Json(_userService.GetById(user.UserId).ToViewModel());
        }
        [HttpPost]
        public JsonResult UpdateUserInfo(UserViewModel user)
        {
            return Json(_userService.UpdateUserInfo(user).ToViewModel());
        }

        [HttpPost]
        public JsonResult Merge(int oldUserId, int newUserId)
        {
            _userService.Merge(oldUserId, newUserId);
            return Json(new { success = true });
        }


    }
    public static class ControllerExtension
    {
        public static string GetDeviceType(this Controller sender)
        {
            string agent = (HttpContext.Current.Request.UserAgent ?? "").ToLower();
            if (agent.IndexOf("andriod", StringComparison.Ordinal) > -1) agent = "andriod";
            if (agent.IndexOf("blackberry", StringComparison.Ordinal) > -1) agent = "blackberry";
            if (agent.IndexOf("ios", StringComparison.Ordinal) > -1) agent = "ios";
            if (agent.Length > 20)
                agent = "ios";
            return agent;
        }
    }
}