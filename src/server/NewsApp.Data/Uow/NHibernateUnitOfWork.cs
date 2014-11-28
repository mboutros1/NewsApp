﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using the template for generating Repositories and a Unit of Work for NHibernate model.
// Code is generated on: 11/21/2014 1:53:39 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace NewsApp.Model
{
    public partial class NHibernateUnitOfWork : IUnitOfWork
    {
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public NHibernateUnitOfWork()
            : this(NHibernateSessionProvider.SessionFactory.OpenSession()) 
        {
        }
        
        public NHibernateUnitOfWork(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            _session = session;
            BeginTransaction();
        }

        public ISession Session
        {
            get
            {
                return _session;
            }
        }

        protected virtual void BeginTransaction()
        {
            if (_session == null)
                throw new InvalidOperationException("Session has not been initialized.");
            _transaction = _session.BeginTransaction();
        }

        private void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        private void CloseSession()
        {
            if (_session != null)
            {
                _session.Close();
                _session.Dispose();
                _session = null;
            }
        }

        #region IDisposable Methods

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    CloseTransaction();
                    CloseSession();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IUnitOfWork Members

        public virtual void Save()
        {
            if (_session == null)
                throw new InvalidOperationException("Session has not been initialized.");
            if (_transaction == null || !_transaction.IsActive)
                throw new InvalidOperationException("No transaction is active.");
            _transaction.Commit();
        }
        #endregion
    }
}
