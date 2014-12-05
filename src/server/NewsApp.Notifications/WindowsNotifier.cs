using NewsAppModel.Services.Providers;
using PushSharp.Windows;

namespace NewsApp.Notifications
{
    public class WindowsNotifier : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification, int badge, string sound, int feedId)
        {
            AppController.PushProBroker.QueueNotification(new WindowsToastNotification()
                .AsToastText01(notification)
                .ForChannelUri(deviceId));
        }


        public string Type
        {
            get { return "ios"; }
        }
    }
}