using System;

namespace NewsAppModel.Services
{
    public class UserViewModel
    {
        private string _avatar;
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long FacebookId { get; set; }
        public string Avatar {
            get {
                if (FacebookId > 0) {
                    return "FB:" + FacebookId;
                }
                if (string.IsNullOrWhiteSpace(_avatar))
                    return "/null.png";
                return _avatar;
            }
            set { _avatar = value; }
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public   bool IsAnonymous
        {
            get;
            set;
        }
    }
}