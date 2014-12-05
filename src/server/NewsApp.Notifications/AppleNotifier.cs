using NewsAppModel.Services.Providers;
using PushSharp;
using PushSharp.Apple;

namespace NewsApp.Notifications
{
    public class AppleNotifier : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification, int badge, string sound,int feedId) {
            int factor = 10;
            if (notification.Length > factor)
                notification = notification.Substring(0, factor);
            if (deviceId.Length < 39)
                return;
            AppController.PushProBroker.QueueNotification(new AppleNotification()
                 .ForDeviceToken(deviceId)
                 .WithAlert(notification)
                 .WithCustomItem("id", feedId)
                 .WithBadge(badge)
                 .WithSound("default"));
        }


        public string Type
        {
            get { return "ios"; }
        }
    }
}