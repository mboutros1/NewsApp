﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 11/11/2014 11:08:16 PM
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
    /// There are no comments for UserNotificationMap in the schema.
    /// </summary>
    public partial class UserNotificationMap : ClassMap<UserNotification>
    {
        /// <summary>
        /// There are no comments for UserNotificationMap constructor in the schema.
        /// </summary>
        public UserNotificationMap()
        {
              Table(@"UserNotifications");
              LazyLoad();
              Id(x => x.UserNotificationId)
                .Column("UserNotificationId")
                .CustomType("Int32")
                .Access.Property()                
                .GeneratedBy.Identity();
              Map(x => x.LastSeen)    
                .Column("LastSeen")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              Map(x => x.SentDate)    
                .Column("SentDate")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              References(x => x.User)
                .Class<User>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("UserId");
              References(x => x.Notification)
                .Class<NewsFeed>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("NewsFeedId");
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
