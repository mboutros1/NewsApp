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

namespace NewsApp.Model
{
    public partial interface IRepository<T>
    {
        System.Linq.IQueryable<T> All();
        void Add(T entity);
        void Remove(T entity);
    }
}
