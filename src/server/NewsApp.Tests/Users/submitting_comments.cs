using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Model;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class submitting_comments : BaseTest
    {
        private FeedService _feedService;
        private INewsFeedRepository _newsFeed;
        private IRepository<User> _userRepository;
        private IUnitOfWork _uoWork;
        [SetUp]
        public void SetThisUp()
        {
            _feedService = Factory.Get<FeedService>();
            _newsFeed = Factory.Get<INewsFeedRepository>();
            _userRepository = Factory.Get<IRepository<User>>();
            _uoWork = Factory.Get<IUnitOfWork>();
        }

        public override void Dispose()
        {
            _uoWork.Save();
            base.Dispose();
        }
        [Test, NUnit.Framework.ExpectedException]
        public void submit_anon_comment_withnull_shouldfail()
        {
            var feed = _newsFeed.All().FirstOrDefault();
            var user = _userRepository.All().FirstOrDefault();
            string comment = null;
            Debug.WriteLine("Starting");
            _feedService.Comment(feed.NewsFeedId, user.UserId, comment);

        }
        [Test]
        public void submit_anon_comment_valid()
        {
            var feed = _newsFeed.All().FirstOrDefault();

            string comment = "this is a comment";
            var user = _userRepository.All().FirstOrDefault();
            Debug.WriteLine("Starting");
            _feedService.Comment(feed.NewsFeedId, user.UserId, comment);

        }

        [Test]
        public void like_a_post()
        {
            var feed = _newsFeed.All().FirstOrDefault();
            var user = _userRepository.All().FirstOrDefault();
            Debug.WriteLine("Starting");
            _feedService.Like(feed.NewsFeedId, user.UserId);

        }
        [Test]
        public void Didlike_a_post()
        {
            //var feed = _newsFeed.All().FirstOrDefault();
            //var user = _userRepository.All().FirstOrDefault();
            Debug.WriteLine("Starting");
            _feedService.Dislike(1000, 109);

        }
        [Test]
        public void Load_a_feed()
        {
         //   var feed = _newsFeed.All().FirstOrDefault();

            var user = _userRepository.All().FirstOrDefault();

        }

        [Test]
        public void like_a_post_and_than_save_it()
        {
            var feed = _newsFeed.All().FirstOrDefault();
            var likesValue = feed.LikesCount;
            var user = _userRepository.All().FirstOrDefault();
            Debug.WriteLine("Starting");
            likesValue.Dump();
            _feedService.Like(feed.NewsFeedId, user.UserId);
            Assert.AreEqual(feed.LikesCount, likesValue++);
            likesValue.Dump();
            feed.LikesCount.Dump();

        }
    }
}
