using System;
using System.Collections.Generic;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Model;
using NewsAppModel.Services.Providers;

namespace NewsAppModel.Services
{
    public class NotificationService
    {
        private readonly IList<IDeviceProvider> _providers;
        private readonly IRepository<UserNotification> _userNotificationRepository;
        private readonly IUnitOfWork _uow;
        private readonly INewsFeedRepository _newsFeedRepository;
        public NotificationService(IList<IDeviceProvider> providers, IRepository<UserNotification> userNotificationRepository, IUnitOfWork uow, INewsFeedRepository newsFeedRepository)
        {
            _providers = providers;
            _userNotificationRepository = userNotificationRepository;
            _uow = uow;
            _newsFeedRepository = newsFeedRepository;
        }

        public void SendNotification(IList<User> users, NewsFeed notification)
        {
            foreach (var user in users)
            {
                foreach (var device in user.Devices)
                {

                    var provider = _providers.FirstOrDefault(m => m.Type == device.Type);
                    if (provider == null)
                        throw new InvalidOperationException("Device Not Supported");
                    provider.SendNotification(device.UserDeviceId, notification.Title, 1, "default");
                    _userNotificationRepository.Add(new UserNotification() { Notification = notification, SentDate = DateTime.Now, User = user });

                }
            }
            _uow.Save();
        }

        public TimeLineResponse GetFeed(int userId, int startId, bool refresh)
        {
            if (!refresh && startId < 0) refresh = true;
            var feed = _newsFeedRepository.GetNewsFeed(userId, startId, refresh);
            return new TimeLineResponse() { data = feed, err_code = 0, err_msg = "" };
        }

        static NotificationService()
        {

        }
    }

    public class TimeLineResponse
    {
        public TimeLineResponse()
        {
            data = new List<NewsFeedView>();
        }

        public int err_code { get; set; }
        public string err_msg { get; set; }
        public IList<NewsFeedView> data { get; set; }
    }
    public class NewsFeedView
    {
        public string Avatar { get; set; }
        public int Id { get; set; }
        public string Images { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int CommentsCount { get; set; }
        public int Likes { get; set; }
        public DateTime CreateDate { get; set; }
    }
}