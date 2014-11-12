using NewsApp.Tests.Common;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class feedbackSubmission : BaseTest
    {
        [SetUp]
        public void SetThisUp()
        {
            svc = Factory.Get<UserService>();
        }

        private UserService svc;

        [Test]
        public void submit_feed_back()
        {
            svc.FeedBack(1, "fadasdfas");
        }
    }
}