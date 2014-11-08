﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using the template for generating Repositories and a Unit of Work for NHibernate model.
// Code is generated on: 11/7/2014 1:09:11 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using NHibernate;
using System.Linq;
using NHibernate.Linq;

namespace NewsApp.Model
{
    public partial class NHibernateRepository<T> : IRepository<T>
    {
        protected ISession session;

        public NHibernateRepository(ISession session)
        {

            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            this.session = session;
        }

        public virtual IQueryable<T> All()
        {

            return (from entity in session.Query<T>() select entity);
        }

        public virtual void Add(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (!session.Transaction.IsActive)
            {
                using (ITransaction transaction = session.BeginTransaction())
				        {
                    session.Save(entity);
                    transaction.Commit();
				        }
            }
            else
                session.Save(entity);
        }

        public virtual void Remove(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (!session.Transaction.IsActive)
            {
                using (ITransaction transaction = session.BeginTransaction())
				        {
                    session.Delete(entity);
                    transaction.Commit();
				        }
            }
            else
                session.Delete(entity);
        }
	}
}
