using System.Linq;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Messaging;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class user_subscripe : BaseTest
    {
        [SetUp]
        public void SetThisUp()
        {
            _churchService = Factory.Get<ChurchService>();
            _churchRepository = Factory.Get<IRepository<Church>>();
            _userRepository = Factory.Get<IRepository<User>>();
            _uoWork = Factory.Get<IUnitOfWork>();
        }

        public override void Dispose()
        {
            _uoWork.Save();
            base.Dispose();
        }

        private IUnitOfWork _uoWork;

        private ChurchService _churchService;
        private IRepository<Church> _churchRepository;
        private IRepository<User> _userRepository;

        //[Test]
        //public void subscribe_for_church_subscription()
        //{
        //    var church = _churchRepository.All().FirstOrDefault();
        //    var user = _userRepository.All().FirstOrDefault();
        //    var churchSubscriptionId = church.ChurchSubscriptions[0].ChurchSubscriptionId;
        //    _churchService.Subscribe(churchSubscriptionId, user.UserId);
        //    _uoWork.Commit();
        //}

        //[Test]
        //public void unsubscribe_for_church_subscription()
        //{
        //    var church = _churchRepository.All().FirstOrDefault();
        //    var user = _userRepository.All().FirstOrDefault();
        //    var churchSubscriptionId = church.ChurchSubscriptions[0].ChurchSubscriptionId;
        //    _churchService.Unsubscribe(churchSubscriptionId, user.UserId);
        //    _uoWork.Commit();
        //}
        [Test]
        public void get_user_subscription()
        {

            var lst = _churchService.GetSubscription(1);
            lst.Dump();
        }
        [Test]
        public void update_user_subscriptions()
        {
            var user = _userRepository.All().FirstOrDefault();
            var request = new UserChurchSubscriptionRequest { UserId = user.UserId };
            var churchRequest = new ChurchRequest { ChurchId = 1 };
            churchRequest.SubscriptionRequests.Add(new ChurchSubscriptionRequest { ChurchSubscriptionId = 1, IsSubscribe = true });
            churchRequest.SubscriptionRequests.Add(new ChurchSubscriptionRequest { ChurchSubscriptionId = 2, IsSubscribe = true });
            request.Churches.Add(churchRequest);
            _churchService.UpdateSubscription(request);
            _uoWork.Commit();
        }
        [Test]
        public void update_user_subscriptions_with_removin()
        {
            var user = _userRepository.All().FirstOrDefault();
            var request = new UserChurchSubscriptionRequest { UserId = user.UserId };
            var churchRequest = new ChurchRequest { ChurchId = 1 };
            churchRequest.SubscriptionRequests.Add(new ChurchSubscriptionRequest { ChurchSubscriptionId = 1, IsSubscribe = true });
            request.Churches.Add(churchRequest);
            _churchService.UpdateSubscription(request);
            _uoWork.Commit();
        }
    }
}