using System.Collections.Generic;
using NewsApp.Model;
using NewsAppModel.Model;
using NewsAppModel.Services;
using NHibernate;
using NHibernate.Transform;

namespace NewsApp.Data
{
    public class NewsFeedRepository : NHibernateRepository<NewsFeed>, INewsFeedRepository
    {
        public NewsFeedRepository(ISession session)
            : base(session)
        {
        }

        public IList<NewsFeedView> GetNewsFeed(int userId, int startId, bool refresh)
        {
            var sql = @"select top {0}  feed.NewsFeedId Id , feed.Images, feed.Title  , u.Avatar ,
u.Name ,feed.LikesCount , feed.CreateDate,  CommentsCount from newsfeeds feed   
 join Users u on feed.userid = u.userid
 WHERE   ChurchSubscriptionId IN ( SELECT    ChurchSubscriptionid
                      FROM      dbo.Users_Subscriptions ch
                      WHERE     ch.userid = {1} )    {2}  order by feed.NewsFeedId desc";
            var str = string.Format(" and feed.newsfeedid {0} {1}  ", refresh ? ">" : "<", startId);
            sql = string.Format(sql, 10, userId, startId == 0 ? "" : str);
            return session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<NewsFeedView>())
                .List<NewsFeedView>();
        }
        public NewsFeedView GetNewsFeed(int feedId)
        {
            var sql = @"select  feed.NewsFeedId Id , feed.Images, feed.Title  , u.Avatar ,
u.Name ,feed.LikesCount , feed.CreateDate,  CommentsCount from newsfeeds feed   
 join Users u on feed.userid = u.userid
 WHERE  eed.NewsFeedId = {0} desc";
            sql = string.Format(sql, feedId);
            return session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<NewsFeedView>())
                .UniqueResult<NewsFeedView>();
        }
        public void LikePost(int newsFeedId)
        {
            session.CreateQuery(
                "UPDATE NewsFeed SET LikesCount = LikesCount +1 WHERE NewsFeedId = :id")
                .SetParameter("id", newsFeedId)
                .ExecuteUpdate();
        }

        public void NotificaitonSeen(int userNotificaitonId)
        {
            session.CreateQuery(
                       "UPDATE UserNotificaiton SET LastSeen = GetUTCDate()  WHERE userNotificaitonId = :id")
                       .SetParameter("id", userNotificaitonId)
                       .ExecuteUpdate();
        }
    }
}