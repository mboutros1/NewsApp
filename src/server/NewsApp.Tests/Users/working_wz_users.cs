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
    public class working_wz_users
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
        public void Doworking_wz_users()
        {
            var deviceId = LocalHelper.Now.ToFileTime().ToString();
            var deviceType = "ios";
            var now = LocalHelper.Now;
            var fst = _userRepository.All().FirstOrDefault(m => m.Devices.Any(h => h.UserDeviceId == deviceId));
            fst = fst ?? new User();
            fst.AddDevice(new UserDevice() { LastLogin = now, UserDeviceId = deviceId, Type = deviceType });
            //; if (fst.DeviceId != deviceId)
            //{
            //    if (fst.UserId != 0)
            //        fst.LastModified = now;
            //    fst.DeviceId = deviceId;
            //}
            //fst.DeviceType = deviceType;
            if (fst.CreateDate == DateTime.MinValue) fst.CreateDate = now;
            //if (fst.UserId == 0)
            _userRepository.Add(fst);
            _uow.Save();
            fst.Dump();
        }
        [Test]
        public void facebook_login()
        {
            var deviceId = LocalHelper.Now.ToFileTime().ToString();
            long facebookId = 123123123;
            var birthdate = "04/12/1990";
            var email = "asdf@fasdf.com";
            var userId = LocalHelper.Now.Second;
            var deviceType = "ios";
            var us = _userService.LoginFb(userId, email, birthdate, facebookId, deviceId, "ios");
            us.Dump();
        }
    }
}
