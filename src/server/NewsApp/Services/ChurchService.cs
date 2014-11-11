using System;
using System.Linq;
using NewsApp.Model;
using NewsAppModel.Messaging;

namespace NewsAppModel.Services
{
    public class ChurchService
    {
        private readonly IRepository<Church> _churchRepository;
        private readonly IRepository<ChurchSubscription> _churchSubscriptionRepository;
        private readonly IRepository<User> _userRepository;

        public ChurchService(IRepository<Church> churchRepository, IRepository<User> userRepository, IRepository<ChurchSubscription> churchSubscriptionRepository)
        {
            _churchRepository = churchRepository;
            _userRepository = userRepository;
            _churchSubscriptionRepository = churchSubscriptionRepository;
        }

        public void AddSubscriptionType(int churchId, ChurchSubscription churchSubscription)
        {
            if (churchId == 0) throw new ArgumentException("churchId");
            var church = _churchRepository.GetById(churchId);
            if (church == null)
                throw new Exception("Church not found");
            church.AddSubscriptionType(churchSubscription);
            _churchRepository.Add(church);
        }

        public void RemoveSubscriptionType(int churchId, int churchSubscriptionId)
        {
            if (churchId == 0) throw new ArgumentException("churchId");
            var church = _churchRepository.GetById(churchId);
            if (church == null)
                throw new Exception("Church not found");
            var subscription =
                church.ChurchSubscriptions.FirstOrDefault(m => m.ChurchSubscriptionId == churchSubscriptionId);
            church.ChurchSubscriptions.Remove(subscription);
            _churchRepository.Add(church);
        }

        public void Subscribe(int churchSubscriptionId, int userId)
        {
            if (churchSubscriptionId == 0)
                throw new ArgumentException("churchSubscriptionId");
            if (userId == 0)
                throw new ArgumentException("userId");
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new InvalidOperationException("User not found");
            var churchsubscription = _churchSubscriptionRepository.GetById(churchSubscriptionId);
            if (churchsubscription == null)
                throw new InvalidOperationException("churchsubscription not found");
            user.Subscriptions.Add(churchsubscription);
            _userRepository.Add(user);
        }

        public void Unsubscribe(int churchSubscriptionId, int userId)
        {
            if (churchSubscriptionId == 0)
                throw new ArgumentException("churchSubscriptionId");
            if (userId == 0)
                throw new ArgumentException("userId");
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new InvalidOperationException("User not found");
            var churchsubscription = _churchSubscriptionRepository.GetById(churchSubscriptionId);
            if (churchsubscription == null)
                throw new InvalidOperationException("churchsubscription not found");
            user.Subscriptions.Remove(user.Subscriptions.FirstOrDefault(m => m.ChurchSubscriptionId == churchSubscriptionId));
            _userRepository.Add(user);
        }

        public void UpdateSubscription(ChurchSubscriptionRequest churchSubscriptionRequest)
        {
            throw new NotImplementedException();
        }
    }
}