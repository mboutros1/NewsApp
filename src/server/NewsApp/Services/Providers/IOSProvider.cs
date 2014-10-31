using System;

namespace NewsAppModel.Services.Providers
{
    public class IOsProvider : IDeviceProvider
    {
        public void SendNotification(string deviceId, string notification)
        {
            throw new NotImplementedException();
        }
        public string Type { get; set; }
    }
}