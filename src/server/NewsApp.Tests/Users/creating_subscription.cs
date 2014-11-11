using System.Linq;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture, RequiresSTA]
    public class creating_subscription : BaseTest
    {
        [SetUp]
        public void SetThisUp()
        {
            _churchRepository = Factory.Get<IRepository<Church>>();
            _churchServices = Factory.Get<ChurchService>();
            _uoWork = Factory.Get<IUnitOfWork>();
        }

        private IRepository<Church> _churchRepository;
        private ChurchService _churchServices;
        private IUnitOfWork _uoWork;

        public override void Dispose()
        {
            _uoWork.Save();
            base.Dispose();
        }

        [Test]
        public void create_new_subscirption()
        {
            var church = _churchRepository.All().FirstOrDefault();

            _churchServices.AddSubscriptionType(church.ChurchId, new ChurchSubscription { Name = "Jobs" });

        }

        [Test]
        public void delete_new_subscirption()
        {
            var church = _churchRepository.All().FirstOrDefault(m => m.ChurchSubscriptions.Count > 0);
            if (church == null)
            {
                church = _churchRepository.All().FirstOrDefault();
                _churchServices.AddSubscriptionType(church.ChurchId, new ChurchSubscription { Name = "Jobs" });
            }
            _churchServices.RemoveSubscriptionType(church.ChurchId, church.ChurchSubscriptions[0].ChurchSubscriptionId);
        }
    }
}