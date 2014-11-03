using System;
using System.Collections.Generic;
using NewsAppModel.Infrastructure;
using NewsAppModel.Services.Providers;

namespace MGB.AppleNotification
{
    public class AppleNotifier : IDeviceProvider
    {
        public INotificationPayload SendNotification(string deviceId, string notification, int badge, string sound)
        {
            var payload1 = new NotificationPayload(deviceId, notification, 1, "default");
            payload1.AddCustom("RegionID", "IDQ10150");
            var push = GetNotification();
            var rejected = push.SendToApple(new List<INotificationPayload> { payload1 });
            foreach (var item in rejected)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
            return payload1;
        }

        public INotificationPayload SendNotification(INotificationPayload notficNotificationPayload)
        {
            var push = GetNotification();
            var rejected = push.SendToApple(new List<INotificationPayload> { notficNotificationPayload });
            foreach (var item in rejected)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
            return notficNotificationPayload;
        }

       
        public void SendNotification(List<INotificationPayload> notficNotificationPayloads)
        {

            var push = GetNotification();
            var rejected = push.SendToApple(notficNotificationPayloads);
            foreach (var item in rejected)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        private PushNotification GetNotification()
        {
#if DEBUG
        return new PushNotification(true, AppSettings.Instance.P12FileLocation, AppSettings.Instance.P12FilePassword);
#else 
        return new PushNotification(false, AppSettings.Instance.P12FileLocation, AppSettings.Instance.P12FilePassword);
#endif

        }
        public string Type { get { return "ios"; } }
    }
}