using System;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Extensions;
using NewsAppModel.Helpers;
using NewsAppModel.Messaging;
using NewsAppModel.Model;

namespace NewsAppModel.Services
{
    public class FeedService
    {
        private readonly IRepository<Church> _churchRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly INewsFeedRepository _newsFeedRepository;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;
        private readonly UserService _userService;

        public FeedService(INewsFeedRepository newsFeedRepository, IUnitOfWork uow, IRepository<User> userRepository,
            IRepository<Church> churchRepository, IRepository<Comment> commentRepository, UserService userService)
        {
            _newsFeedRepository = newsFeedRepository;
            _uow = uow;
            _userRepository = userRepository;
            _churchRepository = churchRepository;
            _commentRepository = commentRepository;
            _userService = userService;
        }
        public NewsFeedView Post(CreateFeedRequest createRequest)
        {
            if (createRequest.ChurchId == 0)
                throw new InvalidOperationException("Church was not set while creating new feed");
            if (createRequest.UserId == 0)
                throw new InvalidOperationException("user was not set while creating new feed");
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == createRequest.UserId);
            if (user == null)
                throw new InvalidOperationException("user can't be found [Post]");
            var church = _churchRepository.All().FirstOrDefault(m => m.ChurchId == createRequest.ChurchId);
            if (church == null)
                throw new InvalidOperationException("Church can't be found [Post]");
            var feed = new NewsFeed
            {
                Body = createRequest.Body,
                Title = createRequest.Title,
                CreateDate = LocalHelper.Now,
                NotifyUsers = createRequest.Notify,
                Images = createRequest.Images,
                CreatedBy = user,
                IsGlobal = createRequest.IsGlobal,
                ScheduleDate = createRequest.ScheduleDate,
                Chruch = church
            };
            _newsFeedRepository.Add(feed);
            _uow.Commit();
            return _newsFeedRepository.GetNewsFeed(feed.NewsFeedId);
        }

        public TimeLineResponse GetFeed(int userId, int startId, bool refresh)
        {
            if (!refresh && startId < 0) refresh = true;
            var feed = _newsFeedRepository.GetNewsFeed(userId, startId, refresh);
            return new TimeLineResponse { data = feed, err_code = 0, err_msg = "" };
        }

        public TimeLineResponse GetInitFeed(int userId, int startId, bool refresh, string deviceId, string deviceType)
        {
            var response = new TimeLineResponse();
            if (userId == 0)
            {
                response.User = _userService.Register(userId, deviceId, deviceType).ToViewModel();
                userId = response.User.UserId;
            }
            if (!refresh && startId < 0) refresh = true;
            response.data = _newsFeedRepository.GetNewsFeed(userId, startId, refresh);
            response.err_code = 0;
            response.err_msg = "";
            return response;
        }

        public NewsFeedDetailView GetDetail(int feedId)
        {
            return _newsFeedRepository.GetById(feedId).ToDetailViewModel();
        }
        public NewsFeedDetailView Comment(int feedId, int userId, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("comment");
            var ct = new Comment
            {
                Body = comment,
                CreateDate = LocalHelper.Now,
                NewsFeed = { NewsFeedId = feedId },
                User = { UserId = userId }
            };
            _commentRepository.Add(ct);
            _uow.Commit();
            return GetDetail(feedId);
            //TODO: Add logging
        }

        public void Like(int feedId, int userId)
        {
            _newsFeedRepository.LikePost(feedId);
            _uow.Commit();

            //TODO: Add logging
        }
    }
}