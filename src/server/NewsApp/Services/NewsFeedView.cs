using System;

namespace NewsAppModel.Services
{
    public class NewsFeedView
    {
        public string Avatar { get; set; }
        public int Id { get; set; }
        public string Images { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public long CommentsCount { get; set; }
        public long LikesCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}