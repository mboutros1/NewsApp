namespace NewsAppModel.Messaging {
    public class TimeLineRequest
    {
        public TimeLineRequest()
        {

        }
        public TimeLineRequest(int userId, int? startAt, bool? refresh, string deviceId, string deviceType)
        {
            UserId = userId;
            StartAt = startAt;
            Refresh = refresh;
            DeviceId = deviceId;
            DeviceType = deviceType;
        }

        public int UserId { get; set; }

        public int? StartAt { get; set; }

        public bool? Refresh { get; set; }

        public string DeviceId { get; set; }

        public string DeviceType { get; set; }
    }
}