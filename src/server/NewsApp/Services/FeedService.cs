using System;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Helpers;
using NewsAppModel.Messaging;
using NewsAppModel.Model;

namespace NewsAppModel.Services
{
    public class FeedService
    {
        private readonly IRepository<Church> _churchRepository;
        private readonly INewsFeedRepository _newsFeedRepository;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;

        public FeedService(INewsFeedRepository newsFeedRepository, IUnitOfWork uow, IRepository<User> userRepository,
            IRepository<Church> churchRepository, IRepository<Comment> commentRepository)
        {
            _newsFeedRepository = newsFeedRepository;
            _uow = uow;
            _userRepository = userRepository;
            _churchRepository = churchRepository;
            _commentRepository = commentRepository;
        }


        public void CreateFeed(CreateFeedRequest createRequest)
        {
            if (createRequest.ChurchId == 0)
                throw new InvalidOperationException("Church was not set while creating new feed");
            if (createRequest.UserId == 0)
                throw new InvalidOperationException("user was not set while creating new feed");
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == createRequest.UserId);
            if (user == null)
                throw new InvalidOperationException("user can't be found [CreateFeed]");
            var church = _churchRepository.All().FirstOrDefault(m => m.ChurchId == createRequest.ChurchId);
            if (church == null)
                throw new InvalidOperationException("Church can't be found [CreateFeed]");
            _newsFeedRepository.Add(new NewsFeed
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
            });
            //_uow.Save();
        }

        public TimeLineResponse GetFeed(int userId, int startId, bool refresh)
        {
            if (!refresh && startId < 0) refresh = true;
            var feed = _newsFeedRepository.GetNewsFeed(userId, startId, refresh);
            return new TimeLineResponse { data = feed, err_code = 0, err_msg = "" };
        }

        public void Comment(int feedId, int userId, string comment)
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
            //TODO: Add logging

        }

        public void Like(int feedId, int userId)
        {
            _newsFeedRepository.LikePost(feedId);
            //TODO: Add logging
        }
    }
}