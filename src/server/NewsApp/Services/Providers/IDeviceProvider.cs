using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsAppModel.Services.Providers
{
    public interface IDeviceProvider
    {
        void SendNotification(string deviceId, string notification);
        string Type { get; }
    }
}
