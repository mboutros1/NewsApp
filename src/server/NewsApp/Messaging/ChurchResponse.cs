using System.Collections.Generic;
using System.Linq;
using NewsApp.Model;

namespace NewsAppModel.Messaging
{
    public class ChurchResponse
    {
        public ChurchResponse()
        {
            Subscription = new List<ChurchSubscriptionResponse>();
        }

        public string Name { get; set; }
        public string Avatar { get; set; }
        public int ChurchId { get; set; }
        public List<ChurchSubscriptionResponse> Subscription { get; set; }
    }

    public class ChurchSubscriptionResponse
    {
        public string Name { get; set; }
        public int ChurchSubscriptionId { get; set; }
        public int ChurchId { get; set; }
        public bool IsSubscribe { get; set; }
    }

    public class UserChurchSubscriptionResponse
    {
        public UserChurchSubscriptionResponse()
        {
            Churches = new List<ChurchResponse>();
        }

        public List<ChurchResponse> Churches { get; set; }

        public override string ToString()
        {
            var output = "";
            foreach (var churchResponse in Churches)
            {
                output += churchResponse.Name + " " + churchResponse.ChurchId + "\r\n";
                output = churchResponse.Subscription.Aggregate(output, (current, sub) => current + (" " + sub.Name + " " + sub.ChurchSubscriptionId + "\r\n"));
            }
            return output;
         }
    }

    public static class Extension
    {
        public static UserChurchSubscriptionResponse ToResponse(this IList<ChurchSubscription> sender, User user)
        {
            var lst = new List<ChurchResponse>();
            var gp = sender.GroupBy(h => h.Church);
            foreach (var k in gp)
            {
                var response = new ChurchResponse {Name = k.Key.DisplayName, ChurchId = k.Key.ChurchId};
                lst.Add(response);
                foreach (var subscription in k)
                {
                    var isSubscribed =
                        user.Subscriptions.Any(h => h.ChurchSubscriptionId == subscription.ChurchSubscriptionId);
                    response.Subscription.Add(new ChurchSubscriptionResponse
                    {
                        ChurchSubscriptionId = subscription.ChurchSubscriptionId,
                        IsSubscribe = isSubscribed,
                        Name = subscription.Name, ChurchId = subscription.Church.ChurchId
                    });
                }
            }
            return new UserChurchSubscriptionResponse {Churches = lst};
        }
    }
}