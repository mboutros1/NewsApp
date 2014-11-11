using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsAppModel.Messaging
{
    public class ChurchRequest
    {

        public string Name { get; set; }
        public int ChurchId { get; set; }
    }

    public class ChurchSubscriptionRequest
    {
        public string Name { get; set; }
        public int ChurchSubscriptionId { get; set; }
        public bool IsSubscribe { get; set; }
    }

    public class UserChurchSubscriptionRequest
    {
        public List<ChurchRequest> Churches { get; set; }

    }
}
