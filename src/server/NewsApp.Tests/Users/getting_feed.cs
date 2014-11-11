using System.Linq;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class getting_feed : BaseTest
    {
        [SetUp]
        public void SetThisUp()
        {
            _feedService = Factory.Get<FeedService>();
            _userRepository = Factory.Get<IRepository<User>>();
            ;
            _uoWork = Factory.Get<IUnitOfWork>();
        }

        private FeedService _feedService;
        private IRepository<User> _userRepository;
        private IUnitOfWork _uoWork;


        public override void Dispose()
        {
            _uoWork.Save();
            base.Dispose();
        }

        [Test]
        public void get_the_feed_for_user()
        {
            var user = _userRepository.All().FirstOrDefault();
            _feedService.GetFeed(user.UserId, 0, true);
        }

        [Test]
        public void refresh_the_feed_for_user()
        {
            var user = _userRepository.All().FirstOrDefault();
            _feedService.GetFeed(user.UserId, 0, true);
        }

        [Test]
        public void scroll_the_feed_for_user()
        {
            var user = _userRepository.All().FirstOrDefault();
            _feedService.GetFeed(user.UserId, 10, true);
        }
    }
}