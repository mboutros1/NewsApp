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
            const string sql = @"select top {0}  feed.NewsFeedId Id , feed.Images, feed.Title  , u.Avatar ,
u.Name ,feed.Likes , feed.CreateDate,
(Select Count(*) from Comments where NewsFeedId =feed.NewsFeedId) CommentsCount from newsfeeds feed   
 join Users u on feed.userid = u.userid
  where ChurchId in ( select Churchid from Users_Churches where userid = {1})    and feed.newsfeedid {2} {3}  order by feed.CreateDate desc";

            return session.CreateSQLQuery(string.Format(sql, 10, userId,refresh ? ">" : "<",  startId))
                .SetResultTransformer(Transformers.AliasToBean<NewsFeedView>())
                .List<NewsFeedView>();
        }
    }
}