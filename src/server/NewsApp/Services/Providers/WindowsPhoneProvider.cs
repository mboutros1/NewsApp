using System;

namespace NewsAppModel.Services.Providers
{
    public class WindowsPhoneProvider : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification)
        {
            throw new NotImplementedException();
        }

        public string Type { get; private set; }
    }
}