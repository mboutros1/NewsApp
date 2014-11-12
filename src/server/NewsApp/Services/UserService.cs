using System;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Helpers;

namespace NewsAppModel.Services
{
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
            var now = LocalHelper.Now;
            var fst = _userRepository.All().FirstOrDefault(m => m.Devices.Any(h => h.UserDeviceId == deviceId));
            fst = fst ?? new User();
            fst.AddDevice(new UserDevice { LastLogin = now, UserDeviceId = deviceId, Type = deviceType });
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
            return fst;
        }

        public User GetById(int userId)
        {
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
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
            var email = "";
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
            foreach (var u in sndUser.Devices)
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
            var chlst =
                sndUser.Churches.Where(
                    m => !mainUser.Churches.Select(h => h.ChurchId).Contains(m.ChurchId));
            foreach (var userSubscription in chlst)
            {
                mainUser.Churches.Add(userSubscription);
            }
            if (mainUser.CreateDate == DateTime.MinValue) mainUser.CreateDate = LocalHelper.Now;
            _userRepository.Remove(sndUser);
            _userRepository.Add(mainUser);
            return mainUser;
        }

        public User LoginFb(LoginRequest loginRequest)
        {
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == loginRequest.UserId);
            if (user == null && !string.IsNullOrEmpty(loginRequest.Email))
                user = _userRepository.All().FirstOrDefault(m => m.Email == loginRequest.Email);

            user = user ?? new User { CreateDate = LocalHelper.Now };
            user.AddDevice(loginRequest.DeviceId, loginRequest.DeviceType);
            user.Email = loginRequest.Email;
            user.Name = loginRequest.Name;
            user.BirthDay = DateTime.Parse(loginRequest.Birthdate);
            if (user.Churches.Count == 0)
            {
                user.AddChurch(1);
            }
            if (!string.IsNullOrEmpty(loginRequest.Email))
            {
                var item = _userRepository.All().FirstOrDefault(m => m.Email == loginRequest.Email);
                if (item != null)
                {
                    user = Merge(user, item);
                }
            }
            _userRepository.Add(user); //user.BirthDay = datetim
            return user;
        }
    }

    public class LoginRequest
    {
        private readonly string _birthdate;
        private readonly int _churchId;
        private readonly string _deviceId;
        private readonly string _deviceType;
        private readonly string _email;
        private readonly long _facebookId;
        private readonly string _name;
        private readonly int _userId;

        public LoginRequest(int userId, string email, string name, string birthdate, long facebookId, string deviceId,
            string deviceType)
            : this(userId, email, name, birthdate, facebookId, deviceId, deviceType, 0)
        {
        }

        public LoginRequest(int userId, string email, string name, string birthdate, long facebookId, string deviceId,
            string deviceType, int churchId)
        {
            _userId = userId;
            _email = email;
            _name = name;
            _birthdate = birthdate;
            _facebookId = facebookId;
            _deviceId = deviceId;
            _deviceType = deviceType;
            _churchId = churchId == 0 ? 1 : churchId;
        }

        public int UserId
        {
            get { return _userId; }
        }

        public int ChurchId
        {
            get { return _churchId; }
        }

        public string Email
        {
            get { return _email; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Birthdate
        {
            get { return _birthdate; }
        }

        public long FacebookId
        {
            get { return _facebookId; }
        }

        public string DeviceId
        {
            get { return _deviceId; }
        }

        public string DeviceType
        {
            get { return _deviceType; }
        }
    }
}