using System.Collections.Generic;
using NewsApp.Model;
using NewsAppModel.Services;

namespace NewsAppModel.Model
{
    public interface INewsFeedRepository : IRepository<NewsFeed>
    {
        IList<NewsFeedView> GetNewsFeed(int userId, int startId, bool refresh);
        void LikePost(int newsFeedId);
        void NotificaitonSeen(int userNotificaitonId);
    }

    public interface ICommentRepository : IRepository<Comment>
    {

    }
}