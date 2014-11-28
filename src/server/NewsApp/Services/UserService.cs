using System;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Extensions;
using NewsAppModel.Helpers;

namespace NewsAppModel.Services
{
    public class UserService
    {
        private readonly IRepository<FeedBack> _feedBackRepository;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;
        private IRepository<ChurchSubscription> _churchSubscriptionRepository;

        public UserService(IRepository<User> userRepository, IUnitOfWork uow, IRepository<FeedBack> feedBackRepository, IRepository<ChurchSubscription> churchSubscriptionRepository)
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
            var isNewUser = fst == null; 
            fst = fst ?? new User();
            if (!string.IsNullOrWhiteSpace(deviceId) && !string.IsNullOrWhiteSpace(deviceType))
                fst.AddDevice(new UserDevice { LastLogin = now, UserDeviceId = deviceId, Type = deviceType });
            if (isNewUser)
            {
                fst.AddChurch(1);
                var lst = _churchSubscriptionRepository.All().Where(m => m.Church.ChurchId == 1).Select(m=>m.ChurchSubscriptionId);
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
            // DataRow dr =  ExecuteReader("Select * from Users where Userid  = @userid
            // User user = new User(); 
            // user.UserId = dr["UserId"]
            // user.Name = dr["Name"]
            // user.Email = dr["Email"]
            //.....
            //" Update Users set email = @email where user = @userid 

            if (user == null)
                throw new InvalidOperationException("User Not found");
            return user;
        }

        public User Merge(int oldUserid, int newUserId) {
            return Merge(_userRepository.All().First(m => m.UserId == oldUserid), _userRepository.All().First(m => m.UserId == newUserId));
        }
        public User Merge(User user1, User user2)
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

        public User Login(LoginRequest loginRequest)
        {
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == loginRequest.UserId);
            if (user == null && !string.IsNullOrEmpty(loginRequest.Email))
                user = _userRepository.All().FirstOrDefault(m => m.Email == loginRequest.Email);

            user = user ?? new User { CreateDate = LocalHelper.Now };
            user.AddDevice(loginRequest.DeviceId, loginRequest.DeviceType);
            user.Email = loginRequest.Email;
            user.Name = loginRequest.Name;
            user.BirthDay = DateTime.Parse(loginRequest.Birthdate);
            user.FacebookId = loginRequest.FacebookId;
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