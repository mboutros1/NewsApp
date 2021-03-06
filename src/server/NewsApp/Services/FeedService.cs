﻿using System;
using System.Collections.Generic;
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
        private readonly IRepository<ChurchSubscription> _churchSubscriptionRepository;
        private readonly IRepository<Church> _churchRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly INewsFeedRepository _newsFeedRepository;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;
        private readonly UserService _userService;

        public FeedService(INewsFeedRepository newsFeedRepository, IUnitOfWork uow, IRepository<User> userRepository,
            IRepository<Church> churchRepository, IRepository<Comment> commentRepository, UserService userService, IRepository<ChurchSubscription> churchSubscriptionRepository)
        {
            _newsFeedRepository = newsFeedRepository;
            _uow = uow;
            _userRepository = userRepository;
            _churchRepository = churchRepository;
            _commentRepository = commentRepository;
            _userService = userService;
            _churchSubscriptionRepository = churchSubscriptionRepository;
        }
        public NewsFeedView Post(CreateFeedRequest createRequest)
        {
            if (createRequest.ChurchId == 0 && createRequest.ChurchSubscriptionId == 0)
                throw new InvalidOperationException("Church was not set while creating new feed");
            if (createRequest.UserId == 0)
                throw new InvalidOperationException("user was not set while creating new feed");
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == createRequest.UserId);
            if (user == null)
                throw new InvalidOperationException("user can't be found [Post]");
            ChurchSubscription churchSub = null;
            if (createRequest.ChurchSubscriptionId != 0)
            {
                churchSub = _churchSubscriptionRepository.All().FirstOrDefault(m => m.ChurchSubscriptionId == createRequest.ChurchSubscriptionId);
            }
            if (churchSub == null)
                throw new InvalidOperationException("ChurchSubscription can't be found [Post]");
            var church = churchSub.Church;
            if (church == null)
                throw new InvalidOperationException("Church can't be found [Post]");
            var feed = new NewsFeed
            {
                Body = "",
                Title = createRequest.Body ?? "",
                CreateDate = LocalHelper.Now,
                NotifyUsers = createRequest.Notify,
                Images = createRequest.Images,
                CreatedBy = user,
                IsGlobal = createRequest.IsGlobal,
                ScheduleDate = createRequest.ScheduleDate,
                Chruch = church,
                ChurchSubscription = churchSub
            };
            _newsFeedRepository.Add(feed);
            _uow.Commit();
            return _newsFeedRepository.GetNewsFeed(feed.NewsFeedId);
        }

        public TimeLineResponse GetFeed(int userId, int startId, bool refresh)
        {
            if (!refresh && startId < 0) refresh = true;
            var vUser = ValidateUser(userId, null, null);
            if (vUser != null) userId = vUser.UserId;

            var feed = _newsFeedRepository.GetNewsFeed(userId, startId, refresh);
            return new TimeLineResponse { data = feed, err_code = 0, err_msg = "" };
        }

        private UserViewModel ValidateUser(int userId, string deviceId, string deviceType)
        {
            return _userService.Register(userId, deviceId, deviceType).ToViewModel();
        }

        public TimeLineResponse GetInitFeed(TimeLineRequest timeLineRequest)
        {
            timeLineRequest.DeviceId = timeLineRequest.DeviceId == "null" ? null : timeLineRequest.DeviceId;
            var response = new TimeLineResponse();
            var vUser = ValidateUser(timeLineRequest.UserId, timeLineRequest.DeviceId, timeLineRequest.DeviceType);
            if (vUser != null) timeLineRequest.UserId = vUser.UserId;
            if (!timeLineRequest.Refresh.GetValueOrDefault() && timeLineRequest.StartAt < 0) timeLineRequest.Refresh = true;
            response.data = _newsFeedRepository.GetNewsFeed(timeLineRequest.UserId, timeLineRequest.StartAt.GetValueOrDefault(), timeLineRequest.Refresh.GetValueOrDefault());
            response.err_code = 0;
            response.err_msg = "";
            response.User = vUser;
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

        public long Like(int feedId, int userId)
        {
            var value = _newsFeedRepository.LikePost(feedId, userId);
            _uow.Commit();
            return value;
            //TODO: Add logging
        }
        public long Dislike(int feedId, int userId)
        {
            var value = _newsFeedRepository.DislikePost(feedId, userId);
            _uow.Commit();
            return value;
            //TODO: Add logging
        }
        public List<CommentView> GetComments(int feedId)
        {
            return
                _newsFeedRepository.GetComments(feedId).ToList();
        }
    }
}