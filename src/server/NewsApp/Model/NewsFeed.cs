namespace NewsApp.Model {
    public partial class NewsFeed {
        public virtual void AddComments(Comment comment) {
            Comments.Add(comment);
            comment.NewsFeed = this;
        }

        public override string ToString() {
            return string.Format("#{0} {1} ", NewsFeedId, Title);
        }
    }
}