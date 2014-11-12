using System.Collections.Generic;

namespace NewsAppModel.Messaging
{
    public class ChurchRequest
    {
        public ChurchRequest()
        {
            SubscriptionRequests = new List<ChurchSubscriptionRequest>();
        }

        public int ChurchId { get; set; }
        public List<ChurchSubscriptionRequest> SubscriptionRequests { get; set; }
    }
    public class ChurchSubscriptionRequest
    {
        public int ChurchSubscriptionId { get; set; }
        public bool IsSubscribe { get; set; }
    }

    public class UserChurchSubscriptionRequest
    {
        public UserChurchSubscriptionRequest()
        {
            Churches = new List<ChurchRequest>();
        }
        public int UserId { get; set; }
        public List<ChurchRequest> Churches { get; set; }
    }

}