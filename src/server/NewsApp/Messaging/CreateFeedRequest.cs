using System;
using NewsApp.Model;

namespace NewsAppModel.Messaging
{
    public class CreateFeedRequest
    {
        public string Body { get; set; }
        public string Title { get; set; }
        public bool Notify { get; set; }
        public int ChurchSubscriptionId { get; set; }
        public int UserId { get; set; }
        public int ChurchId { get; set; }
        public bool IsEvent { get; set; }
        public string Images { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public bool? IsGlobal { get; set; }
    }
}