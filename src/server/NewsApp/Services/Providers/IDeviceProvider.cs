namespace NewsAppModel.Services.Providers
{
    public interface IDeviceProvider
    {
        string Type { get; }

        void SendNotification(string deviceId, string notification, int badge,
            string sound);
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