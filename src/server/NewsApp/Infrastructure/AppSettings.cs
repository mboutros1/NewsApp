using System;
using System.Collections.Specialized;

namespace NewsAppModel.Infrastructure
{
    public class AppSettings
    {
        private static AppSettings _instance;

        private NameValueCollection _settings;

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("App Settings is not initialized");
                return _instance;
            }
            private set { _instance = value; }
        }

        public string ConnectionString
        {
            get { return _settings["NewsAppConnectionString"]; }
        }
        public string P12FileLocation
        {
            get { return _settings["P12FileLocation"]; }
        }
        public string P12FilePassword
        {
            get { return _settings["P12FilePassword"]; }
        }
        public static void Init(NameValueCollection settings)
        {
            Instance = new AppSettings {_settings = settings};
        }
    }
}