using System;
using AutoMapper;
using NewsApp.Model;

namespace NewsAppModel.Services
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
    public static class UserExtension
    {
        static UserExtension()
        {
            Mapper.CreateMap<User, UserViewModel>().ReverseMap();
        }

        public static UserViewModel ToViewModel(this User sender)
        {
            if (sender == null) return null;
            return Mapper.Map<User, UserViewModel>(sender);
        }
    }
}