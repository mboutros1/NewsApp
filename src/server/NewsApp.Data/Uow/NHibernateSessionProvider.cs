﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using the template for generating Repositories and a Unit of Work for NHibernate model.
// Code is generated on: 11/3/2014 10:50:18 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;

namespace NewsApp.Model
{
    public class NHibernateSessionProvider
    {

        private static Configuration configuration;
        private static ISessionFactory sessionFactory;

        private NHibernateSessionProvider()
        {
        }

        public static Configuration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = new Configuration();
                    configuration.Configure();
                }
                return configuration;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = FluentNHibernate.Cfg.Fluently.Configure(Configuration).Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateSessionProvider>()).BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        public static ISession GetSession()
        {

            return SessionFactory.OpenSession();
        }
    }
}
