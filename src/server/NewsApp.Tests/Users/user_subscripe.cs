using System.Linq;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Messaging;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class user_subscripe:BaseTest
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

        [Test]
        public void subscribe_for_church_subscription()
        {
            Church church = _churchRepository.All().FirstOrDefault();
            User user = _userRepository.All().FirstOrDefault();
            int churchSubscriptionId = church.ChurchSubscriptions[0].ChurchSubscriptionId;
            _churchService.Subscribe(churchSubscriptionId, user.UserId);
        }

        [Test]
        public void unsubscribe_for_church_subscription()
        {
            Church church = _churchRepository.All().FirstOrDefault();
            User user = _userRepository.All().FirstOrDefault();
            int churchSubscriptionId = church.ChurchSubscriptions[0].ChurchSubscriptionId;
            _churchService.Unsubscribe(churchSubscriptionId, user.UserId);
        }

        [Test]
        public void update_user_subscriptions()
        {
            Church church = _churchRepository.All().FirstOrDefault();
            User user = _userRepository.All().FirstOrDefault();
            _churchService.UpdateSubscription(new ChurchSubscriptionRequest());
        }
    }
}