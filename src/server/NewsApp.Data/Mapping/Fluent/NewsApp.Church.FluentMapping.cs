﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 11/21/2014 1:53:38 PM
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
    /// There are no comments for ChurchMap in the schema.
    /// </summary>
    public partial class ChurchMap : ClassMap<Church>
    {
        /// <summary>
        /// There are no comments for ChurchMap constructor in the schema.
        /// </summary>
        public ChurchMap()
        {
              Table(@"Churches");
              LazyLoad();
              Id(x => x.ChurchId)
                .Column("ChurchId")
                .CustomType("Int32")
                .Access.Property()                
                .GeneratedBy.Identity();
              Map(x => x.DisplayName)    
                .Column("DisplayName")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Address)    
                .Column("Address")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.ZipCode)    
                .Column("ZipCode")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.State)    
                .Column("State")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.City)    
                .Column("City")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Country)    
                .Column("Country")
                .CustomType("String")
                .Access.Property()
                .Generated.Never()
                .Default(@"'US'");
              Map(x => x.Latitude)    
                .Column("Latitude")
                .CustomType("Int64")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Longitude)    
                .Column("Longitude")
                .CustomType("Int64")
                .Access.Property()
                .Generated.Never();
              Map(x => x.LiveStremUrl)    
                .Column("LiveStremUrl")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              HasMany<User>(x => x.HomeUsers)
                .Access.Property()
                .AsBag()
                .Cascade.SaveUpdate()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .NotFound.Ignore()
                .Inverse()
                .Generic()
                .KeyColumns.Add("ChurchId", mapping => mapping.Name("ChurchId")
                                                                     .SqlType("int")
                                                                     .Nullable());
              HasManyToMany<User>(x => x.Users)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .Table("Churches_Users")
                .FetchType.Join()
                .ChildKeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .Nullable())
                .ParentKeyColumns.Add("ChurchId", mapping => mapping.Name("ChurchId")
                                                                     .Nullable());
              HasMany<NewsFeed>(x => x.Feeds)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("ChurchId", mapping => mapping.Name("ChurchId")
                                                                     .Nullable());
              HasMany<ChurchSubscription>(x => x.ChurchSubscriptions)
                .Access.Property()
                .AsBag()
                .Cascade.AllDeleteOrphan()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("ChurchId", mapping => mapping.Name("ChurchId")
                                                                     .Nullable());
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
