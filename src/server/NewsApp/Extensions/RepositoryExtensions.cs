using System.Linq;
using NewsApp.Model;

namespace NewsAppModel.Extensions
{
    public static class RepositoryExtensions
    {
        public static Church GetById(this IRepository<Church> sender,int churchId)
        {
            return sender.All().FirstOrDefault(m => m.ChurchId == churchId);
        }
        public static User GetById(this IRepository<User> sender, int userId)
        {
            return sender.All().FirstOrDefault(m => m.UserId == userId);
        }
        public static ChurchSubscription GetById(this IRepository<ChurchSubscription> sender, int churchSubscriptionId)
        {
            return sender.All().FirstOrDefault(m => m.ChurchSubscriptionId == churchSubscriptionId);
        }
        public static NewsFeed GetById(this IRepository<NewsFeed> sender, int newsFeedId)
        {
            return sender.All().FirstOrDefault(m => m.NewsFeedId == newsFeedId);
        }
        public static UserDevice GetById(this IRepository<UserDevice> sender, string deviceId)
        {
            return sender.All().FirstOrDefault(m => m.UserDeviceId == deviceId);
        }
    }
}
