using System.Web.Mvc;
using NewsAppModel.Messaging;
using NewsAppModel.Services;

namespace NewsApp.Controllers
{
    public class NewsFeedController : Controller
    {
        private readonly FeedService _feedService;
        private readonly UserService _userService;

        public NewsFeedController(FeedService feedService, UserService userService)
        {
            _userService = userService;
            _feedService = feedService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFeed(int userId, int? startAt, bool? refresh)
        {
            refresh = refresh ?? true;
            return Json(_feedService.GetFeed(userId, startAt.GetValueOrDefault(), refresh.GetValueOrDefault()),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInitFeed(int userId, int? startAt, bool? refresh, string deviceId, string deviceType)
        {
            refresh = refresh ?? true;
            return
                Json(
                    _feedService.GetInitFeed(userId, startAt.GetValueOrDefault(), refresh.GetValueOrDefault(), deviceId,
                        deviceType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Comment(int userId, int feedId, string comment)
        {
            return Json(_feedService.Comment(feedId, userId, comment), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Like(int userId, int feedId)
        {
            _feedService.Like(feedId, userId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Post(CreateFeedRequest request)
        {
            return Json(_feedService.Post(request), JsonRequestBehavior.AllowGet);
        }
    }
}