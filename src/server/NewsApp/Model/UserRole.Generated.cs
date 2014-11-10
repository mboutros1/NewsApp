  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.UserRole, NewsAppModel in the schema.
    /// </summary>
    public partial class UserRole {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for UserRole constructor in the schema.
        /// </summary>
        public UserRole()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for UserRoleId in the schema.
        /// </summary>
        public virtual int UserRoleId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CanPost in the schema.
        /// </summary>
        public virtual bool CanPost
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for User in the schema.
        /// </summary>
        public virtual User User
        {
            get;
            set;
        }
    }

}
