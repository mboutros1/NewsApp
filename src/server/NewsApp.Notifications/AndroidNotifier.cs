using NewsAppModel.Services.Providers;
using PushSharp;
using PushSharp.Android;

namespace NewsApp.Notifications
{
    public class AndroidNotifier : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification, int badge, string sound)
        {
            AppController.PushProBroker.QueueNotification(new GcmNotification()
                .ForDeviceRegistrationId(deviceId)
                .WithJson("{\"alert\":\"" + notification + "\",\"badge\":7,\"sound\":\"" + sound + "\"}"));
        }


        public string Type
        {
            get { return "ios"; }
        }
    }
}