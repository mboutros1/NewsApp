using System;

namespace NewsApp.Model
{
    public partial class Church
    {
        public virtual void AddSubscriptionType(ChurchSubscription subscription)
        {
            if (subscription == null)
                throw new ArgumentException("subscription");
            ChurchSubscriptions.Add(subscription);
            subscription.Church = this;
        }
    }
}