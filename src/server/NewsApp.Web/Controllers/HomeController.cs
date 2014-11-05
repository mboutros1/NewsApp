using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApp.Model;
using NewsAppModel.Services;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly NotificationService _notificationService;
        private readonly UserService _userService;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        public HomeController(NotificationService notificationService, UserService userService, IRepository<Notification> notificationRepository, IRepository<User> userRepository, IUnitOfWork uow)
        {
            _notificationService = notificationService;
            _userService = userService;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Register(int? userId, string deviceId, string deviceType)
        {
            return Json(_userService.Register(userId.GetValueOrDefault(), deviceId, deviceType), JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestSendById(int userId)
        {
            var user = _userService.GetById(userId);
            var notification = _notificationRepository.All().FirstOrDefault();
            if (notification == null)
                throw new InvalidOperationException("no notifications in the database, please create one");
            _notificationService.SendNotification(user, notification);
            return View();
        }
        public ActionResult TestSend()
        {
            var users = _userRepository.All().ToList();
            if (users.Count == 0)
                throw new InvalidOperationException("no users in the database, please create one");
            var notification = _notificationRepository.All().FirstOrDefault();
            if (notification == null)
                throw new InvalidOperationException("no notifications in the database, please create one");
            foreach (var user in users)
                _notificationService.SendNotification(user, notification);
            _uow.Save();
            return View();
        }
    }
}
