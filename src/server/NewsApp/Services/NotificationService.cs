using System;
using System.Collections.Generic;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Services.Providers;

namespace NewsAppModel.Services
{
    public class NotificationService
    {
        private readonly IList<IDeviceProvider> _providers;
        private readonly IRepository<UserNotification> _userNotificationRepository;
        private readonly IUnitOfWork _uow;
        public NotificationService(IList<IDeviceProvider> providers, IRepository<UserNotification> userNotificationRepository, IUnitOfWork uow)
        {
            _providers = providers;
            _userNotificationRepository = userNotificationRepository;
            _uow = uow;
        }

        public void SendNotification(User user, Notification notification)
        {
            IDeviceProvider provider = _providers.FirstOrDefault(m => m.Type == user.DeviceType);
            if (provider == null)
                throw new InvalidOperationException("Device Not Supported");
            provider.SendNotification(user.DeviceId, notification.Title);
            _userNotificationRepository.Add(new UserNotification(){Notification = notification,SendDate = DateTime.Now,User = user});
            _uow.Save();
        }

    }
}