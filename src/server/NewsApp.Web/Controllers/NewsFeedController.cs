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

        public JsonResult GetFeed(TimeLineRequest timeLineRequest)
        {
            timeLineRequest.Refresh = timeLineRequest.Refresh ?? true;
            return Json(_feedService.GetFeed(timeLineRequest.UserId, timeLineRequest.StartAt.GetValueOrDefault(), timeLineRequest.Refresh.GetValueOrDefault()),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFeedDetails(int id)
        {
            return Json(_feedService.GetDetail(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInitFeed(TimeLineRequest timeLineRequest)
        {
            timeLineRequest.Refresh = timeLineRequest.Refresh ?? true;
            return
               Json(
                   _feedService.GetInitFeed(timeLineRequest), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Comment(int userId, int feedId, string comment)
        {
            return Json(_feedService.Comment(feedId, userId, comment), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetComments(int feedId)
        {
            return Json(_feedService.GetComments(feedId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Like(int userId, int feedId)
        {
            return Json(new { Count = _feedService.Like(feedId, userId) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Dislike(int userId, int feedId)
        {
            ;
            return Json(new { Count = _feedService.Dislike(feedId, userId) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Post(CreateFeedRequest request)
        {
            return Json(new[] { _feedService.Post(request) }, JsonRequestBehavior.AllowGet);
        }
    }
}