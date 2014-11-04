﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using the template for generating Repositories and a Unit of Work for NHibernate model.
// Code is generated on: 11/3/2014 10:50:18 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using System;
using NHibernate;

namespace NewsApp.Model
{
    public partial class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
    {
        protected ISession _session = null;

        public NHibernateUnitOfWorkFactory()
            : this(NHibernateSessionProvider.SessionFactory.OpenSession())
        {
        }
        
        public NHibernateUnitOfWorkFactory(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            _session = session;
        }

        #region IUnitOfWorkFactory Members

        public virtual IUnitOfWork Create()
        {
            if (_session == null)
                throw new InvalidOperationException("Session has not been initialized.");
            return new NHibernateUnitOfWork(_session);
        }
        #endregion
    }
}
