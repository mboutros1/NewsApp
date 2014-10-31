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
    /// There are no comments for NotificationMap in the schema.
    /// </summary>
    public partial class NotificationMap : ClassMap<Notification>
    {
        /// <summary>
        /// There are no comments for NotificationMap constructor in the schema.
        /// </summary>
        public NotificationMap()
        {
              Table(@"Notifications");
              LazyLoad();
              Id(x => x.NotificationId)
                .Column("NotificationId")
                .CustomType("Int32")
                .Access.Property()
                .GeneratedBy.Assigned();
              Map(x => x.Title)    
                .Column("Title")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Details)    
                .Column("Details")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.CreateDate)    
                .Column("CreateDate")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Type)    
                .Column("Type")
                .CustomType("Int32")
                .Access.Property()
                .Generated.Never();
              HasMany<UserNotification>(x => x.UserNotification)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Not.Generic()
                .KeyColumns.Add("NotificationId", mapping => mapping.Name("NotificationId")
                                                                     .Nullable());
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
