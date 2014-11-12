using System;

namespace NewsAppModel.Services
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}