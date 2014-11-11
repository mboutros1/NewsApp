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

     

        static NotificationService()
        {

        }
    }
}