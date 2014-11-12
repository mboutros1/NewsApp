﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 11/12/2014 1:38:44 PM
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
                .GeneratedBy.Identity();
              Map(x => x.Email)    
                .Column("Email")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.CreateDate)    
                .Column("CreateDate")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              Map(x => x.LastModified)    
                .Column("LastModified")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Avatar)    
                .Column("Avatar")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Name)    
                .Column("Name")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.BirthDay)    
                .Column("BirthDay")
                .CustomType("Date")
                .Access.Property()
                .Generated.Never();
              Map(x => x.FacebookId)    
                .Column("FacebookId")
                .CustomType("Int64")
                .Access.Property()
                .Generated.Never();
              References(x => x.HomeChurch)
                .Class<Church>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .NotFound.Ignore()
                .Columns("ChurchId");
              HasMany<UserNotification>(x => x.Notifications)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              HasManyToMany<Church>(x => x.Churches)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .Table("Users_Churches")
                .FetchType.Join()
                .ChildKeyColumns.Add("ChurchId", mapping => mapping.Name("ChurchId")
                                                                     .Nullable())
                .ParentKeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              HasMany<Comment>(x => x.Comments)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              HasMany<NewsFeed>(x => x.CreatedNotifications)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              HasMany<UserDevice>(x => x.Devices)
                .Access.Property()
                .AsBag()
                .Cascade.AllDeleteOrphan()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              HasManyToMany<ChurchSubscription>(x => x.Subscriptions)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Generic()
                .Table("Users_Subscriptions")
                .FetchType.Join()
                .ChildKeyColumns.Add("ChurchSubscriptionId", mapping => mapping.Name("ChurchSubscriptionId")
                                                                     .Nullable())
                .ParentKeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable());
              HasMany<UserRole>(x => x.Roles)
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
