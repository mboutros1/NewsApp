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
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly INewsFeedRepository _newsFeedRepository;
        public NotificationService(IList<IDeviceProvider> providers, IRepository<UserNotification> userNotificationRepository, IUnitOfWork uow, INewsFeedRepository newsFeedRepository, IRepository<User> userRepository)
        {
            _providers = providers;
            _userNotificationRepository = userNotificationRepository;
            _uow = uow;
            _newsFeedRepository = newsFeedRepository;
            _userRepository = userRepository;
        }

        public void Seen(int userNotificaitonId)
        {
            _newsFeedRepository.NotificationSeen(userNotificaitonId);
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
                    provider.SendNotification(device.UserDeviceId, notification.Title, 1, "default",notification.NewsFeedId);
                    _userNotificationRepository.Add(new UserNotification() { Notification = notification, SentDate = DateTime.Now, User = user });

                }
            }
            _uow.Save();
        }
        /// <summary>
        /// Get the unsent news feed, get the subscriped users for this news feed type, create a user notification for all the subscribres 
        /// </summary>
        public void CreatePendingNotification()
        {
            var pending = _newsFeedRepository.All().Where(m => m.IsSent == false);
            var usersQuery = _userRepository.All();
            List<int> allUserId = null;
            foreach (var newsFeed in pending)
            {
                if (newsFeed.IsGlobal == true)
                {
                    allUserId = allUserId ?? usersQuery.Select(m => m.UserId).ToList();
                    foreach (var i in allUserId)
                    {
                        _userNotificationRepository.Add(new UserNotification()
                        {
                            User = new User() { UserId = i },
                            Notification = new NewsFeed() { NewsFeedId = newsFeed.NewsFeedId }
                        });
                    }
                }
                else
                {
                    var thisUsers =
                        _userRepository.All()
                            .Where(
                                m =>
                                    m.Subscriptions.Select(h => h.ChurchSubscriptionId)
                                        .Contains(newsFeed.ChurchSubscription.ChurchSubscriptionId)).Select(m => m.UserId);
                    foreach (var i in thisUsers)
                    {
                        _userNotificationRepository.Add(new UserNotification()
                        {
                            User = new User() { UserId = i },
                            Notification = new NewsFeed() { NewsFeedId = newsFeed.NewsFeedId }
                        });
                    }
                }
            }
        }
        public void SendPendingNotification()
        {
            var pending = _userNotificationRepository.All().Where(m => m.SentDate == null);
            foreach (var note in pending)
            {
                //SendNotification();
            }
        }
        static NotificationService()
        {

        }
    }

    public class NotificationRunResponse
    {
        public int NotificationCreated { get; set; }
        public int EventNotificationCreated { get; set; }
    }
}