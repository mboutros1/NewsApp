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

        public static void Init(NameValueCollection settings)
        {
            Instance = new AppSettings {_settings = settings};
        }
    }
}