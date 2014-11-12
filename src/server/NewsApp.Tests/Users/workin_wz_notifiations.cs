using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class workin_wz_notifiations : BaseTest
    {
        private IUnitOfWork _uow;
        private NotificationService svc;
        [SetUp]
        public void SetThisUp()
        {
            svc = Factory.Get<NotificationService>();
            _uow = Factory.Get<IUnitOfWork>();
        }

        public override void Dispose()
        {
            _uow.Save();
            base.Dispose();
        }

        [Test]
        public void Create_pending_notification()
        {
            svc.CreatePendingNotification();
        }
        [Test]
        public void send_pending_notification()
        {
            svc.SendPendingNotification();

        }
    }
}
