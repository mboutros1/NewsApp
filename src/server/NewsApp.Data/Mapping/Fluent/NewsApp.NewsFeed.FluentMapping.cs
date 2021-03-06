﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 12/15/2014 3:18:08 PM
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
    /// There are no comments for NewsFeedMap in the schema.
    /// </summary>
    public partial class NewsFeedMap : ClassMap<NewsFeed>
    {
        /// <summary>
        /// There are no comments for NewsFeedMap constructor in the schema.
        /// </summary>
        public NewsFeedMap()
        {
              Table(@"NewsFeeds");
              LazyLoad();
              Id(x => x.NewsFeedId)
                .Column("NewsFeedId")
                .CustomType("Int32")
                .Access.Property()                
                .GeneratedBy.Identity();
              Map(x => x.Body)    
                .Column("Body")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.Title)    
                .Column("Title")
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
              Map(x => x.LikesCount)    
                .Column("LikesCount")
                .CustomType("Int64")
                .Access.Property()
                .Generated.Never()
                .Default(@"0");
              Map(x => x.Images)    
                .Column("Images")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.ScheduleDate)    
                .Column("ScheduleDate")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              Map(x => x.IsSent)    
                .Column("IsSent")
                .CustomType("Boolean")
                .Access.Property()
                .Generated.Never()
                .Default(@"0");
              Map(x => x.NotifyUsers)    
                .Column("NotifyUsers")
                .CustomType("Boolean")
                .Access.Property()
                .Generated.Never()
                .Default(@"0");
              Map(x => x.IsGlobal)    
                .Column("IsGlobal")
                .CustomType("Boolean")
                .Access.Property()
                .Generated.Never();
              Map(x => x.CommentsCount)    
                .Column("CommentsCount")
                .CustomType("Int64")
                .Access.Property()
                .Generated.Never()
                .Default(@"0");
              References(x => x.CreatedBy)
                .Class<User>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("UserId");
              HasManyToMany<User>(x => x.Users)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Generic()
                .Schema("dbo")
                .Table("LikedNewsFeeds_Users")
                .FetchType.Join()
                .ChildKeyColumns.Add("UserId", mapping => mapping.Name("UserId")
                                                                     .SqlType("int")
                                                                     .Not.Nullable())
                .ParentKeyColumns.Add("NewsFeedId", mapping => mapping.Name("NewsFeedId")
                                                                     .SqlType("int")
                                                                     .Not.Nullable());
              References(x => x.Chruch)
                .Class<Church>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("ChurchId");
              References(x => x.ChurchSubscription)
                .Class<ChurchSubscription>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("ChurchSubscriptionId");
              HasMany<UserNotification>(x => x.Notification)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Not.Generic()
                .KeyColumns.Add("NewsFeedId", mapping => mapping.Name("NewsFeedId")
                                                                     .Nullable());
              HasMany<Comment>(x => x.Comments)
                .Access.Property()
                .AsBag()
                .Cascade.None()
                .LazyLoad()
                // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
                .Inverse()
                .Generic()
                .KeyColumns.Add("NewsFeedId", mapping => mapping.Name("NewsFeedId")
                                                                     .Nullable());
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
