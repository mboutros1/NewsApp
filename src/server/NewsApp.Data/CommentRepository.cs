using NewsApp.Model;
using NewsAppModel.Model;
using NHibernate;

namespace NewsApp.Data
{
    public class CommentRepository : NHibernateRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ISession session)
            : base(session)
        {
        }

        public override void Add(Comment entity)
        {
            if (entity.CommentId == 0)
                session.CreateQuery("UPDATE NewsFeed set CommentsCount = CommentsCount + 1 where NewsFeedId =:id")
                    .SetParameter("id", entity.NewsFeed.NewsFeedId)
                    .ExecuteUpdate();
            base.Add(entity);
        }
    }
}