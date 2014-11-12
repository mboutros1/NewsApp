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
        private readonly FeedService _feedService;
        private readonly UserService _userService;
        private readonly IRepository<NewsFeed> _notificationRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        public HomeController(NotificationService notificationService, UserService userService,
            IRepository<NewsFeed> notificationRepository, IRepository<User> userRepository, IUnitOfWork uow, FeedService feedService)
        {
            _notificationService = notificationService;
            _userService = userService;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _uow = uow;
            _feedService = feedService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Register(int? userId, string deviceId, string deviceType)
        {
            return Json(_userService.Register(userId.GetValueOrDefault(), deviceId, deviceType).ToViewModel(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFeed(int userId, int? startAt, bool? refresh)
        {
            refresh = refresh ?? true;
            return Json(_feedService.GetFeed(userId, startAt.GetValueOrDefault(), refresh.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInitFeed(int userId, int? startAt, bool? refresh, string deviceId, string deviceType)
        {
            refresh = refresh ?? true;
            return Json(_feedService.GetInitFeed(userId, startAt.GetValueOrDefault(), refresh.GetValueOrDefault(), deviceId, deviceType), JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestSendById(int userId)
        {
            var user = _userService.GetById(userId);
            var notification = _notificationRepository.All().FirstOrDefault();
            if (notification == null)
                throw new InvalidOperationException("no notifications in the database, please create one");
            _notificationService.SendNotification(new User[] { user }, notification);
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
                _notificationService.SendNotification(users, notification);
            _uow.Save();
            return View();
        }
    }
}
