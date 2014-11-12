﻿using System;
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

        public void UpdateSubscription(UserChurchSubscriptionRequest churchSubscriptionRequest)
        {
            var user = _userRepository.GetById(churchSubscriptionRequest.UserId);
            if (user == null)
                throw new ArgumentOutOfRangeException("Userid not found");

            var subscriptions = churchSubscriptionRequest.Churches.SelectMany(m => m.SubscriptionRequests).Where(m => m.IsSubscribe);
            var removeThose =
                user.Subscriptions.Where(
                    m => !subscriptions.Select(h => h.ChurchSubscriptionId).Contains(m.ChurchSubscriptionId));
            var addThose =
                          subscriptions.Where(
                              m => !user.Subscriptions.Select(h => h.ChurchSubscriptionId).Contains(m.ChurchSubscriptionId));

        fst: foreach (var churchSubscription in removeThose)
            {
                user.Subscriptions.Remove(churchSubscription);
                goto fst;
            }
            foreach (var subscriptionRequest in addThose)
            {
                var newSUbscription = new ChurchSubscription() { ChurchSubscriptionId = subscriptionRequest.ChurchSubscriptionId };

                user.Subscriptions.Add(newSUbscription);
            }
            _userRepository.Add(user);
        }
        public UserChurchSubscriptionResponse GetSubscription(int userid)
        {
            var user = _userRepository.GetById(userid);
            if (user == null)
                throw new ArgumentOutOfRangeException("userid");
            return user.Subscriptions.ToResponse(user);
        }
    }
}