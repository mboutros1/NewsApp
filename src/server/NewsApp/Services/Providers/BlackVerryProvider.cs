using System;

namespace NewsAppModel.Services.Providers
{
    public class BlackVerryProvider : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification)
        {
            throw new NotImplementedException();
        }

        public string Type { get; private set; }
    }
}