using System;
using System.Collections.Generic;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Helpers;

namespace NewsAppModel.Services
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }

    public class UserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;

            _uow = uow;
        }

        public User Register(int userId, string deviceId, string deviceType)
        {
            DateTime now = LocalHelper.Now;
            User fst = _userRepository.All().FirstOrDefault(m => m.Devices.Any(h => h.UserDeviceId == deviceId));
            fst = fst ?? new User();
            fst.AddDevice(new UserDevice {LastLogin = now, UserDeviceId = deviceId, Type = deviceType});
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
            return fst;
        }

        public User GetById(int userId)
        {
            User user = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
            if (user == null)
                throw new InvalidOperationException("User Not found");
            return user;
        }

        private User Merge(User user1, User user2)
        {
            if (user1.UserId == user2.UserId) return user1;
            User mainUser = user1, sndUser = user2;
            if (user1.UserId == 0 && user2.UserId > 0)
            {
                mainUser = user2;
                sndUser = user1;
            }
            string email = "";
            DateTime? bdate;
            if (!string.IsNullOrEmpty(mainUser.Email) && string.IsNullOrEmpty(sndUser.Email))
                email = mainUser.Email;
            else
                email = sndUser.Email;
            if (mainUser.BirthDay != null && sndUser.BirthDay == null)
                bdate = mainUser.BirthDay;
            else
                bdate = sndUser.BirthDay;
            mainUser.Email = email;
            mainUser.BirthDay = bdate;
            foreach (UserDevice u in sndUser.Devices)
            {
                u.User = mainUser;
            }
            //IEnumerable<ChurchSubscription> subscriptionLst =
            //    sndUser.Subscriptions.Where(
            //        m => !mainUser.Subscriptions.Select(h => h.SubscriptionType).Contains(m.SubscriptionType));
            //foreach (ChurchSubscription userSubscription in subscriptionLst)
            //{
            //    userSubscription.User = mainUser;
            //    mainUser.Subscriptions.Add(userSubscription);
            //}
            IEnumerable<Church> chlst =
                sndUser.Churches.Where(
                    m => !mainUser.Churches.Select(h => h.ChurchId).Contains(m.ChurchId));
            foreach (Church userSubscription in chlst)
            {
                mainUser.Churches.Add(userSubscription);
            }
            if (mainUser.CreateDate == DateTime.MinValue) mainUser.CreateDate = LocalHelper.Now;
            _userRepository.Remove(sndUser);
            _userRepository.Add(mainUser);
            return mainUser;
        }

        public User LoginFb(int userId, string email, string name,string birthdate, long facebookId, string deviceId,
            string deviceType)
        {
            User user = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
            if (user == null && !string.IsNullOrEmpty(email))
                user = _userRepository.All().FirstOrDefault(m => m.Email == email);

            user = user ?? new User {CreateDate = LocalHelper.Now};
            user.AddDevice(deviceId, deviceType);
            user.Email = email;
            user.Name = name;
            user.BirthDay = DateTime.Parse(birthdate);
            if (user.Churches.Count == 0)
            {
                user.AddChurch(1);
            }
            if (!string.IsNullOrEmpty(email))
            {
                User item = _userRepository.All().FirstOrDefault(m => m.Email == email);
                if (item != null)
                {
                    user = Merge(user, item);
                }
            }
            _userRepository.Add(user); //user.BirthDay = datetim
            _uow.Save();
            return user;
        }
    }
}