﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 10/31/2014 12:20:10 AM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace NewsApp.Model
{
    /// <summary>
    /// There are no comments for UserMap in the schema.
    /// </summary>
    public partial class UserMap : ClassMap<User>
    {
        /// <summary>
        /// There are no comments for UserMap constructor in the schema.
        /// </summary>
        public UserMap()
        {
              Table(@"Users");
              LazyLoad();
              Id(x => x.UserId)
                .Column("UserId")
                .CustomType("Int32")
                .Access.Property()
                .GeneratedBy.Assigned();
              Map(x => x.DeviceId)    
                .Column("DeviceId")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              HasMany<UserNotification>(x => x.UserNotifications)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
