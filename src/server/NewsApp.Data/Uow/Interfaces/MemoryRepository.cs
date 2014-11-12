﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using the template for generating Repositories and a Unit of Work for NHibernate model.
// Code is generated on: 11/12/2014 1:38:44 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsApp.Model
{
    public partial class MemoryRepository<T> : IRepository<T>
    {
        public MemoryRepository()
        {
        }

        public MemoryRepository(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    Add(entity);
                }
            }
        }

        public MemoryRepository(params T[] entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    Add(entity);
                }
            }
        }

        protected List<T> objectSet = new List<T>();

        public virtual IQueryable<T> All()
        {
            return objectSet.AsQueryable();
        }

        public virtual void Add(T entity)
        {
            objectSet.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            objectSet.Remove(entity);
        }
	  }
}
