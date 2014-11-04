using System;
using System.Linq;
using NewsApp.Model;

namespace NewsAppModel.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            ;
            _uow = uow;
        }

        public User Register(int userId, string deviceId, string deviceType)
        {
            User fst = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
            fst = fst ?? new User { DeviceId = deviceId };
            var now = DateTime.Now;
            ; if (fst.DeviceId != deviceId)
            {
                if (fst.UserId != 0)
                    fst.LastModified = now;
                fst.DeviceId = deviceId;
            }
            fst.DeviceType = deviceType;
            if (fst.CreateDate == DateTime.MinValue) fst.CreateDate = now;
            //if (fst.UserId == 0)
                _userRepository.Add(fst);
            _uow.Save();
            return fst;
        }

        public User GetById(int userId)
        {
            var user = _userRepository.All().FirstOrDefault(m => m.UserId == userId);
            if (user == null)
                throw new InvalidOperationException("User Not found");
            return user;
        }
    }
}