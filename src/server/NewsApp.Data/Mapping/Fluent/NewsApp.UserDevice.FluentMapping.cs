﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate Fluent Mapping template.
// Code is generated on: 11/7/2014 1:09:11 PM
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
    /// There are no comments for UserDeviceMap in the schema.
    /// </summary>
    public partial class UserDeviceMap : ClassMap<UserDevice>
    {
        /// <summary>
        /// There are no comments for UserDeviceMap constructor in the schema.
        /// </summary>
        public UserDeviceMap()
        {
              Table(@"UserDevices");
              LazyLoad();
              Id(x => x.UserDeviceId)
                .Column("UserDeviceId")
                .CustomType("String")
                .Access.Property()
                .GeneratedBy.Assigned();
              Map(x => x.Type)    
                .Column("Type")
                .CustomType("String")
                .Access.Property()
                .Generated.Never();
              Map(x => x.LastLogin)    
                .Column("LastLogin")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never();
              References(x => x.User)
                .Class<User>()
                .Access.Property()
                .Cascade.All()
                .LazyLoad()
                .Columns("UserId");
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}