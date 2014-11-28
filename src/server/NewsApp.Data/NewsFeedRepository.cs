using System.Collections.Generic;
using System.Linq;
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
            var sql = @"SELECT TOP {0}
                            feed.NewsFeedId Id ,
                            feed.Images ,
                            feed.Title ,
                            u.Avatar ,
                            u.Name ,
                            feed.LikesCount ,
                            feed.CreateDate ,
                            CommentsCount ,Cast ( case when NOT exists  (Select     NewsFeedId from LikedNewsFeeds_Users l where l.NewsFeedId = feed.NewsFeedId and l.UserId = {1}) then 0 else 1 end as bit)   IsLiked
                     FROM   NewsFeeds feed
                            JOIN Users u ON feed.UserId = u.UserId
                     WHERE   ChurchSubscriptionId IN ( SELECT    ChurchSubscriptionid
                     FROM      dbo.Subscriptions_Users ch
                     WHERE     ch.userid = {1} )    {2}  order by feed.NewsFeedId desc";
            var str = string.Format(" and feed.newsfeedid {0} {1}  ", refresh ? ">" : "<", startId);
            sql = string.Format(sql, 10, userId, startId == 0 ? "" : str);
            return session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<NewsFeedView>())
                .List<NewsFeedView>();
        }
        public NewsFeedView GetNewsFeed(int feedId)
        {
            var sql = @"SELECT  feed.NewsFeedId Id ,
                                feed.Images ,
                                feed.Title ,
                                u.Avatar ,
                                u.Name ,
                                feed.LikesCount ,
                                feed.CreateDate ,
                                CommentsCount
                        FROM    NewsFeeds feed
                                JOIN Users u ON feed.UserId = u.UserId
                        WHERE   feed.NewsFeedId = {0}  ";
            sql = string.Format(sql, feedId);
            return session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<NewsFeedView>())
                .UniqueResult<NewsFeedView>();
        }
        public long LikePost(int newsFeedId, int userId)
        {
            session.CreateQuery(
                "UPDATE NewsFeed SET LikesCount = LikesCount +1 WHERE NewsFeedId = :id ")
                .SetParameter("id", newsFeedId)
                .ExecuteUpdate();
  session.CreateSQLQuery(
                       @"MERGE dbo.LikedNewsFeeds_Users AS target
                        USING
                            ( SELECT    :userId ,
                                        :id
                            ) AS src ( userid, feedid )
                        ON ( target.NewsFeedId = src.feedid
                             AND target.UserId = src.userid
                           )
                        WHEN NOT MATCHED THEN
                            INSERT ( UserId, NewsFeedId )
                            VALUES ( src.userid ,
                                     src.feedid
           );")
                       .SetParameter("id", newsFeedId)
                    .SetParameter("userId", userId)
                       .ExecuteUpdate();
            return session.CreateQuery("Select LikesCount from NewsFeed WHERE NewsFeedId = :id ")
                .SetParameter("id", newsFeedId).UniqueResult<long>();
        }

        public long DislikePost(int newsFeedId, int userId)
        {
            session.CreateSQLQuery(
                       "UPDATE NewsFeeds SET LikesCount = LikesCount -1 WHERE NewsFeedId = :id and LikesCount >0; Delete from LikedNewsFeeds_Users where UserId=:userId and NewsFeedId=:id")
                       .SetParameter("id", newsFeedId)
                    .SetParameter("userId", userId)
                       .ExecuteUpdate();
            //var user = new User() { UserId = userId };//session.Load<User>(userId);}
            //user.LikedNewsFeeds.Remove(user.LikedNewsFeeds.FirstOrDefault(m => m.NewsFeedId == newsFeedId));
            //session.SaveOrUpdate(user);
            //session.CreateQuery(
            //        "Delete from User.LikedNewsFeed Where NewsFeedId =:newsFeedId and User.UserId=:userId ")
            //        .SetParameter("newsFeedId", newsFeedId)
            //        .SetParameter("userId", userId)
            //        .ExecuteUpdate();
           
            return session.CreateQuery("Select LikesCount from NewsFeed WHERE NewsFeedId = :id ")
          .SetParameter("id", newsFeedId).UniqueResult<long>();
        }

        public IList<CommentView> GetComments(int newsFeedId)
        {
            var sql = @"SELECT  c.Body ,
                                c.Images ,
                                c.CreateDate ,
                                u.Avatar ,
                                u.Name, c.CommentId Id
                        FROM    dbo.Comments c
                                JOIN Users u ON c.UserId = u.UserId
                        WHERE   c.NewsFeedId = {0}
                        Order By CommentId Desc
 ";
            sql = string.Format(sql, newsFeedId);
            return session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean<CommentView>())
                .List<CommentView>();
        }

        public void NotificationSeen(int userNotificationId)
        {
            session.CreateQuery(
                       "UPDATE UserNotificaiton SET LastSeen = GetUTCDate()  WHERE userNotificationId = :id")
                       .SetParameter("id", userNotificationId)
                       .ExecuteUpdate();
        }
    }
}