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
    /// There are no comments for UserRoleMap in the schema.
    /// </summary>
    public partial class UserRoleMap : ClassMap<UserRole>
    {
        /// <summary>
        /// There are no comments for UserRoleMap constructor in the schema.
        /// </summary>
        public UserRoleMap()
        {
              Table(@"UserRoles");
              LazyLoad();
              Id(x => x.UserRoleId)
                .Column("UserRoleId")
                .CustomType("Int32")
                .Access.Property()                
                .GeneratedBy.Identity();
              Map(x => x.CanPost)    
                .Column("CanPost")
                .CustomType("Boolean")
                .Access.Property()
                .Generated.Never();
              References(x => x.User)
                .Class<User>()
                .Access.Property()
                .Cascade.None()
                .LazyLoad()
                .Columns("UserId");
              ExtendMapping();
        }

        #region Partial Methods

        partial void ExtendMapping();

        #endregion
    }

}
