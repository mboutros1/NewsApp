using System;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Extensions;
using NewsAppModel.Helpers;
using NewsAppModel.Model;

namespace NewsAppModel.Services
{
    public class UserService
    {
        private readonly IRepository<FeedBack> _feedBackRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ChurchSubscription> _churchSubscriptionRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork uow, IRepository<FeedBack> feedBackRepository, IRepository<ChurchSubscription> churchSubscriptionRepository)
        {
            _userRepository = userRepository;
            _uow = uow;
            _feedBackRepository = feedBackRepository;
            _churchSubscriptionRepository = churchSubscriptionRepository;
        }

        public User Register(int userId, string deviceId, string deviceType)
        {
            var now = LocalHelper.Now;
            var fst = _userRepository.All().FirstOrDefault(m => m.Devices.Any(h => h.UserDeviceId == deviceId));
            if (fst == null) fst = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
            var isNewUser = fst == null;
            fst = fst ?? new User();
            if (!string.IsNullOrWhiteSpace(deviceId) && !string.IsNullOrWhiteSpace(deviceType))
            {
                fst.AddDevice(new UserDevice { LastLogin = now, UserDeviceId = deviceId, Type = deviceType });
            }
            if (isNewUser)
            {
                fst.AddChurch(1);
                var lst = _churchSubscriptionRepository.All().Where(m => m.Church.ChurchId == 1).Select(m => m.ChurchSubscriptionId);
                foreach (var churchSubscription in lst)
                {
                    fst.AddChurchSubscription(churchSubscription);
                }
            }            //; if (fst.DeviceId != deviceId)
            //{
            //    if (fst.UserId != 0)
            //        fst.LastModified = now;
            //    fst.DeviceId = deviceId;
            //}
            //fst.DeviceType = deviceType;
            if (fst.CreateDate == DateTime.MinValue) fst.CreateDate = now;
            //if (fst.UserId == 0)
            _userRepository.Add(fst);
            return fst;
        }

        public User GetById(int userId)
        {
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
            if (user == null)
                throw new InvalidOperationException("User Not found");
            return user;
        }

        public User Merge(int oldUserid, int newUserId)
        {

            return Merge(_userRepository.All().FirstOrDefault(m => m.UserId == oldUserid), _userRepository.All().FirstOrDefault(m => m.UserId == newUserId));
        }
        public User Merge(User user1, User user2)
        {
            if (user1 == null && user2 != null)
                return user2;
            if (user2 == null && user1 != null)
                return user1;
            if (user1.UserId == user2.UserId) return user1;
            User mainUser = user1, sndUser = user2;
            if (user1.UserId == 0 && user2.UserId > 0)
            {
                mainUser = user2;
                sndUser = user1;
            }
            if (!user2.IsAnonymous && user1.IsAnonymous)
            {
                mainUser = user2;
                sndUser = user1;
            }
            _userRepository.Merge(sndUser.UserId, mainUser.UserId);
            string email;
            DateTime? birthDate;
            if (!string.IsNullOrEmpty(mainUser.Email) && string.IsNullOrEmpty(sndUser.Email))
                email = mainUser.Email;
            else
                email = sndUser.Email;
            if (mainUser.BirthDay != null && sndUser.BirthDay == null)
                birthDate = mainUser.BirthDay;
            else
                birthDate = sndUser.BirthDay;
            mainUser.Email = email;
            mainUser.BirthDay = birthDate;
            mainUser.IsAnonymous = mainUser.IsAnonymous && sndUser.IsAnonymous;
            if (mainUser.CreateDate == DateTime.MinValue) mainUser.CreateDate = LocalHelper.Now;
            _userRepository.Remove(sndUser);
            _userRepository.Add(mainUser);
            return mainUser;
        }

        public User Login(LoginRequest loginRequest)
        {
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == loginRequest.UserId);
            if (user == null && !string.IsNullOrEmpty(loginRequest.Email))
                user = _userRepository.All().FirstOrDefault(m => m.Email == loginRequest.Email);
            if (!string.IsNullOrWhiteSpace(loginRequest.Email))
            {
                var item = _userRepository.All().FirstOrDefault(m => m.Email == loginRequest.Email);
                if (item != null)
                {
                    user = Merge(item, user);
                }
            }
            user = user ?? new User { CreateDate = LocalHelper.Now };
            user.AddDevice(loginRequest.DeviceId, loginRequest.DeviceType);
            user.Email = loginRequest.Email;
            user.Name = loginRequest.Name;
            user.BirthDay = DateTime.Parse(loginRequest.Birthdate);
            user.FacebookId = loginRequest.FacebookId;
            user.IsAnonymous = user.FacebookId == 0;
            if (user.Churches.Count == 0)
            {
                user.AddChurch(1);
            }
            _userRepository.Add(user);
            return user;
        }

        public void FeedBack(int userId, string feedBack)
        {
            _feedBackRepository.Add(new FeedBack
            {
                Body = feedBack,
                CreateDate = LocalHelper.Now,
                User = new User { UserId = userId }
            });
        }

        public User UpdateUserInfo(UserViewModel userVm)
        {
            var user = _userRepository.GetById(userVm.UserId);
            user.Email = userVm.Email;
            user.Name = userVm.Name;
            _userRepository.Add(user);
            _uow.Commit();
            return user;
        }
    }
}