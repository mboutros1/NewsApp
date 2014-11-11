using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Messaging;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture, RequiresSTA]
    public class CreateNewsFeed:BaseTest
    {
        private FeedService _feedService;
        private IRepository<Church> _churchRepository;
        private IRepository<User> _userRepository;
        private IUnitOfWork _uoWork;
        [SetUp]
        public void SetThisUp()
        {
            _feedService = Factory.Get<FeedService>();
            _churchRepository = Factory.Get<IRepository<Church>>();
            _userRepository = Factory.Get<IRepository<User>>();
            _uoWork = Factory.Get<IUnitOfWork>();
        }

        public override void Dispose()
        {
            _uoWork.Save();
            base.Dispose();
        }

        [Test]
        public void For_All_Churches_Wz_notification()
        {
            SetThisUp();
            var church = _churchRepository.All().FirstOrDefault();
            var user = _userRepository.All().FirstOrDefault();
            _feedService.CreateFeed(new CreateFeedRequest
            {
                ChurchId = church.ChurchId,
                Body = "body",
                IsEvent = false,
                Notify = true,
                Title = "title",
                UserId = user.UserId,IsGlobal =true
            });
        }

        [Test]
        public void For_All_Churches_WzOut_notification()
        {
            SetThisUp();
            var church = _churchRepository.All().FirstOrDefault();
            var user = _userRepository.All().FirstOrDefault();
            _feedService.CreateFeed(new CreateFeedRequest
            {
                ChurchId = church.ChurchId,
                Body = "body",
                IsEvent = false,
                Notify = true,
                Title = "title",
                UserId = user.UserId,
                IsGlobal = true
            });
        }
        [Test]
        public void For_Specific_Churches_Wz_notificaiton()
        {
            SetThisUp();
            var user = _userRepository.All().FirstOrDefault();
            var church = _churchRepository.All().FirstOrDefault();
            _feedService.CreateFeed(new CreateFeedRequest
            {
                ChurchId = church.ChurchId,
                Body = "body",
                IsEvent = false,
                Notify = true,
                Title = "title",
                UserId = user.UserId
            });
        }
        [Test]
        public void For_Specific_Churches_Wzout_notificaiton()
        {
            SetThisUp();
            var user = _userRepository.All().FirstOrDefault();
            var church = _churchRepository.All().FirstOrDefault();
            _feedService.CreateFeed(new CreateFeedRequest
            {
                ChurchId = church.ChurchId,
                Body = "body",
                Title = "title",
                UserId = user.UserId
            });
        }

        [Test]
        public void For_Churches_event_Wz_notification()
        {
            SetThisUp();
            var user = _userRepository.All().FirstOrDefault();
            var church = _churchRepository.All().FirstOrDefault();
            _feedService.CreateFeed(new CreateFeedRequest
            {
                ChurchId = church.ChurchId,
                Body = "body",
                IsEvent = true,
                Title = "title",
                UserId = user.UserId
            });
        }

        [Test]
        public void For_Churches_event_WzOut_notification()
        {
            SetThisUp();
            var user = _userRepository.All().FirstOrDefault();
            var church = _churchRepository.All().FirstOrDefault();
            _feedService.CreateFeed(new CreateFeedRequest
            {
                ChurchId = church.ChurchId,
                Body = "body",
                IsEvent = true,
                Title = "title",
                UserId = user.UserId
            });
        }

    }
}
