using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Helpers;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture]
    public class working_wz_login
    {
        [SetUp]
        public void SetThisUp()
        {
            _userRepository = Factory.Get<IRepository<User>>();
            _uow = Factory.Get<IUnitOfWork>();
            _userService = Factory.Get<UserService>();
        }

        private IRepository<User> _userRepository;
        private IUnitOfWork _uow;
        private UserService _userService;

        [Test]
        public void login_wz_facebook()
        {
            var deviceId = LocalHelper.Now.ToFileTime().ToString();
            const string deviceType = "ios";
            var now = LocalHelper.Now;
            var fst = _userRepository.All().FirstOrDefault(m => m.Devices.Any(h => h.UserDeviceId == deviceId));
            fst = fst ?? new User();
            fst.AddDevice(new UserDevice() { LastLogin = now, UserDeviceId = deviceId, Type = deviceType });
            if (fst.CreateDate == DateTime.MinValue) fst.CreateDate = now;
            _userRepository.Add(fst);
            _uow.Save();
            fst.Dump();
        }

        [Test]
        public void login_anon_user()
        {
            
        }
        [Test]
        public void facebook_login()
        {
            var deviceId = LocalHelper.Now.ToFileTime().ToString();
            const long facebookId = 123123123;
            const string birthdate = "04/12/1990";
            const string email = "asdf@fasdf.com";
            var userId = LocalHelper.Now.Second;
            const string deviceType = "ios";
            var us = _userService.LoginFb(userId, email, "", birthdate, facebookId, deviceId, deviceType);
            us.Dump();
        }
    }
}
