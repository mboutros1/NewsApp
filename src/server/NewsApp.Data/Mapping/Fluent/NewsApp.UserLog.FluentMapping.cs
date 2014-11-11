﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 11/11/2014 5:20:31 PM
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
    /// There are no comments for UserLogMap in the schema.
    /// </summary>
    public partial class UserLogMap : ClassMap<UserLog>
    {
        /// <summary>
        /// There are no comments for UserLogMap constructor in the schema.
        /// </summary>
        public UserLogMap()
        {
              Table(@"UserLogs");
              LazyLoad();
              Id(x => x.UserLogId)
                .Column("UserLogId")
                .CustomType("Int32")
                .Access.Property()                
                .GeneratedBy.Identity();
              Map(x => x.LogTime)    
                .Column("LogTime")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              Map(x => x.LogType)    
                .Column("LogType")
                .CustomType("NewsApp.Model.LogType, NewsAppModel")
                .Access.Property()
                .Generated.Never();
              References(x => x.User)
                .Class<User>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("UserId");
              References(x => x.NewsFeed)
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
