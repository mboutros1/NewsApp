using System.Collections.Generic;
using NewsApp.Model;
using NewsAppModel.Services;

namespace NewsAppModel.Model
{
    public interface INewsFeedRepository : IRepository<NewsFeed>
    {
        IList<NewsFeedView> GetNewsFeed(int userId, int startId, bool refresh);
        NewsFeedView GetNewsFeed(int feedId);
        long LikePost(int newsFeedId, int userId);
        long DislikePost(int newsFeedId, int userId);
        IList<CommentView> GetComments(int newsFeedId);
        void NotificationSeen(int userNotificationId);
    }

    public interface ICommentRepository : IRepository<Comment>
    {

    }
}