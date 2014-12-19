namespace NewsApp.Model {
    public partial class UserNotification {
        public override string ToString() {
            return string.Format("#{0} for user:{1} and Feed {2} ", UserNotificationId,
                Notification == null ? "Null" : Notification.NewsFeedId.ToString(), User == null ? "Null" : User.UserId.ToString());
        }
    }
}