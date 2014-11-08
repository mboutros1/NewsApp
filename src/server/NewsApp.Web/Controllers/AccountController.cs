using System;
using System.Web.Mvc;
using AutoMapper;
using NewsApp.Model;
using NewsAppModel.Services;

namespace NewsApp.Controllers
{
    public static class UserExtension
    { 
        static UserExtension()
        {
            Mapper.CreateMap<User, UserViewModel>().ReverseMap();
        }

        public static UserViewModel ToViewModel(this User sender)
        {
            return Mapper.Map<User, UserViewModel>(sender);
        }
    }

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

        [HttpPost]
        public JsonResult LoginFb(int userId, string email, string birthdate, long facebookId, string deviceId)
        {
            return
               Json(_userService.LoginFb(userId, email, birthdate, facebookId, deviceId, GetDeviceType()).ToViewModel());
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

        public string GetDeviceType()
        {
            string agent = (HttpContext.Request.UserAgent ?? "").ToLower();
            if (agent.IndexOf("andriod", StringComparison.Ordinal) > -1) agent = "andriod";
            if (agent.IndexOf("blackberry", StringComparison.Ordinal) > -1) agent = "blackberry";
            if (agent.IndexOf("ios", StringComparison.Ordinal) > -1) agent = "ios";
            if (agent.Length > 20)
                agent = "ios";
            return agent;
        }
    }
}