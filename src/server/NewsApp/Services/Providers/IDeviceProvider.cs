using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsAppModel.Services.Providers
{
    public interface IDeviceProvider
    {
        INotificationPayload SendNotification(string deviceId, string notification, int badge,
            string sound);
        INotificationPayload SendNotification(INotificationPayload notficNotificationPayload);
        void SendNotification(List<INotificationPayload> notficNotificationPayload);
        string Type { get; }
    }

    public static class DeviceProviderExtension
    {
        //public static void SendNotification(this IDeviceProvider sender, string deviceId, string notification, int badge,
        //    string sound)
        //{
        //    sender.SendNotification(new );
        //}

    }
}
