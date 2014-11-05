using NewsAppModel.Services.Providers;
using PushSharp;
using PushSharp.Apple;

namespace NewsApp.Notifications
{
    public class AppleNotifier : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification, int badge, string sound)
        {
            AppController.PushProBroker.QueueNotification(new AppleNotification()
                 .ForDeviceToken(deviceId)
                 .WithAlert(notification)
                 .WithBadge(1)
                 .WithSound("default"));
        }


        public string Type
        {
            get { return "ios"; }
        }
    }
}