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

        public JsonResult GetInitFeed(GetInitFeedParams getInitFeedParams)
        {
            getInitFeedParams.Refresh = getInitFeedParams.Refresh ?? true;
            return
                Json(
                    _feedService.GetInitFeed(getInitFeedParams.UserId, getInitFeedParams.StartAt.GetValueOrDefault(), getInitFeedParams.Refresh.GetValueOrDefault(), getInitFeedParams.DeviceId,
                        getInitFeedParams.DeviceType), JsonRequestBehavior.AllowGet);
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

    public class GetInitFeedParams
    {
        private int _userId;
        private int? _startAt;
        private bool? _refresh;
        private string _deviceId;
        private string _deviceType;

        public GetInitFeedParams()
        {
            
        }
        public GetInitFeedParams(int userId, int? startAt, bool? refresh, string deviceId, string deviceType)
        {
            _userId = userId;
            _startAt = startAt;
            _refresh = refresh;
            _deviceId = deviceId;
            _deviceType = deviceType;
        }

        public int UserId
        {
            get { return _userId; }
        }

        public int? StartAt
        {
            get { return _startAt; }
        }

        public bool? Refresh
        {
            get { return _refresh; }
            set { _refresh = value; }
        }

        public string DeviceId
        {
            get { return _deviceId; }
        }

        public string DeviceType
        {
            get { return _deviceType; }
        }
    }
}