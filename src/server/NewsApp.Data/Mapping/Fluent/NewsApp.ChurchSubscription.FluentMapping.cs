﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 12/1/2014 1:25:35 PM
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
    /// There are no comments for ChurchSubscriptionMap in the schema.
    /// </summary>
    public partial class ChurchSubscriptionMap : ClassMap<ChurchSubscription>
    {
        /// <summary>
        /// There are no comments for ChurchSubscriptionMap constructor in the schema.
        /// </summary>
        public ChurchSubscriptionMap()
        {
              Table(@"ChurchSubscriptions");
              LazyLoad();
              Id(x => x.ChurchSubscriptionId)
                .Column("ChurchSubscriptionId")
                .CustomType("Int32")
                .Access.Property()                
                .GeneratedBy.Identity();
              Map(x => x.Name)    
                .Column("Name")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.IsEvent)    
                .Column("IsEvent")
                .CustomType("Boolean")
                .Access.Property()
                .Generated.Never();
              HasManyToMany<User>(x => x.Users)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .Table("Subscriptions_Users")
                .ChildKeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable())
                .ParentKeyColumns.Add("ChurchSubscriptionId", mapping => mapping.Name("ChurchSubscriptionId")
                                                                     .Nullable());
              HasMany<NewsFeed>(x => x.NewsFeeds)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("ChurchSubscriptionId", mapping => mapping.Name("ChurchSubscriptionId")
                                                                     .Nullable());
              References(x => x.Church)
                .Class<Church>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("ChurchId");
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
