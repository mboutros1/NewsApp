using System.Collections.Generic;
using NewsAppModel.Services;

namespace NewsAppModel.Messaging
{
    public class TimeLineResponse
    {
        public TimeLineResponse()
        {
            data = new List<NewsFeedView>();
        }

        public int err_code { get; set; }
        public string err_msg { get; set; }
        public IList<NewsFeedView> data { get; set; }
        public UserViewModel User { get; set; }
    }
}