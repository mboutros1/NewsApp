namespace NewsAppModel.Services
{
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