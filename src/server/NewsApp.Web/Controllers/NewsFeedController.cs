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

        public JsonResult GetFeed(GetFeedRequest getFeedRequest)
        {

            getFeedRequest.Refresh = getFeedRequest.Refresh ?? true;
            return Json(_feedService.GetFeed(getFeedRequest.UserId, getFeedRequest.StartAt.GetValueOrDefault(), getFeedRequest.Refresh.GetValueOrDefault()),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInitFeed(GetFeedRequest getFeedRequest)
        {
            getFeedRequest.Refresh = getFeedRequest.Refresh ?? true;
            return
               Json(
                   _feedService.GetInitFeed(getFeedRequest.UserId, getFeedRequest.StartAt.GetValueOrDefault(), getFeedRequest.Refresh.GetValueOrDefault(), getFeedRequest.DeviceId,
                       getFeedRequest.DeviceType), JsonRequestBehavior.AllowGet);
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
            return Json(_feedService.Post(request), JsonRequestBehavior.AllowGet);
        }
    }

    public class GetFeedRequest
    {
        public GetFeedRequest()
        {

        }
        public GetFeedRequest(int userId, int? startAt, bool? refresh, string deviceId, string deviceType)
        {
            UserId = userId;
            StartAt = startAt;
            Refresh = refresh;
            DeviceId = deviceId;
            DeviceType = deviceType;
        }

        public int UserId { get; set; }

        public int? StartAt { get; set; }

        public bool? Refresh { get; set; }

        public string DeviceId { get; set; }

        public string DeviceType { get; set; }
    }
}