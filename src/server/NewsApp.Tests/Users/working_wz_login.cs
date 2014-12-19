using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Model;
using NewsApp.Tests.Common;
using NewsAppModel.Helpers;
using NewsAppModel.Model;
using NewsAppModel.Services;
using NUnit.Framework;

namespace NewsApp.Tests.Users
{
    [TestFixture, RequiresSTA]
    public class working_wz_login : BaseTest
    {
        [SetUp]
        public void SetThisUp()
        {
            _userRepository = Factory.Get<IUserRepository>();
            _userService = Factory.Get<UserService>();
            _uoWork = Factory.Get<IUnitOfWork>();
        }
        public override void Dispose()
        {
            _uoWork.Save();
            base.Dispose();
        }
        private IUserRepository _userRepository;
        private UserService _userService;
        private IUnitOfWork _uoWork;

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
            fst.Dump();
        }

        [Test]
        public void register_new_device()
        {
            string deviceId = Path.GetRandomFileName();
            _userService.Register(0, deviceId, "ios");
        }
        [Test]
        public void register_existed_device()
        {
            string deviceId = Path.GetRandomFileName();
            _userService.Register(1, deviceId, "ios");
        }
        [Test]
        public void facebook_login()
        {
            var deviceId = LocalHelper.Now.ToFileTime().ToString();
            const long facebookId = 123123123;
            const string birthdate = "04/12/1990";
            const string email = "mic_louis@hotmail.com";
            var userId = LocalHelper.Now.Second;
            const string deviceType = "ios";
            var us = _userService.Login(new LoginRequest(userId, email, "Mike GB", birthdate, facebookId, deviceId, deviceType));
            us.Dump();
        }

        [Test]
        public void load_all()
        {
            _userRepository.All().ToList();
        }
        [Test]
        public void merge_users()
        {
            _userService.Login(new LoginRequest(117, "mic_louis@hotmail.com", "Mike GB", "04/16/1982", (long)123123999099923, null, null));
        }
        [Test]
        public void merge_users_urepo()
        {
            _userRepository.Merge(117, 175);
            //_userService.Login(new LoginRequest(117, "mic_louis@hotmail.com", "Mike GB", "04/16/1982", (long)123123999099923, null, null));
        }
        [Test]
        public void merge_users_bug_1()
        {
            _userService.Merge(416, 175);
            //_userService.Login(new LoginRequest(117, "mic_louis@hotmail.com", "Mike GB", "04/16/1982", (long)123123999099923, null, null));
        }
    }
}
