using System.Collections.Generic;

namespace NewsAppModel.Services.Providers
{
    public interface INotificationPayload
    {
        NotificationAlert Alert { get; set; }
        string DeviceToken { get; set; }
        int? Badge { get; set; }
        int PayloadId { get; set; }

        string Sound { get; set; }
        Dictionary<string, object[]> CustomItems { get; }
        void AddCustom(string key, params object[] values);
        string ToJson();
        string ToString();
    }
}