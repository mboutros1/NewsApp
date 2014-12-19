using System.Linq;
using NewsAppModel.Helpers;

namespace NewsApp.Model
{
    public partial class User
    {
        public virtual void AddDevice(string deviceId, string deviceType)
        {
            AddDevice(new UserDevice() { UserDeviceId = deviceId, LastLogin = LocalHelper.Now, Type = deviceType });
        }
        public virtual void AddDevice(UserDevice device)
        {
            if (string.IsNullOrWhiteSpace(device.UserDeviceId))
                return;
            if (Devices.Any(m => m.UserDeviceId == device.UserDeviceId)) return;
            device.User = this;
            Devices.Add(device);
        }

        public virtual void AddChurch(int churchId)
        {
            if (Churches.Any(m => m.ChurchId == churchId)) return;
            Churches.Add(new Church() { ChurchId = 1 });

        }
        public virtual void AddChurchSubscription(int churchSubscriptionId)
        {
            if (Subscriptions.Any(m => m.ChurchSubscriptionId == churchSubscriptionId)) return;
            Subscriptions.Add(new ChurchSubscription() { ChurchSubscriptionId = churchSubscriptionId });
        }
        public virtual void AddNotification(NewsFeed feed)
        {
            if (Notifications.Any(m => m.Notification.NewsFeedId == feed.NewsFeedId)) return;
            var note = new UserNotification() { Notification = feed, User = this };
            Notifications.Add(note);

        }
        public override string ToString()
        {
            return string.Format("#{0} {1} Anon:{2} ", UserId, Name, IsAnonymous);
        }

    }
}