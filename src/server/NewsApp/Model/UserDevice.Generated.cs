  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.UserDevice, NewsAppModel in the schema.
    /// </summary>
    public partial class UserDevice {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for UserDevice constructor in the schema.
        /// </summary>
        public UserDevice()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for UserDeviceId in the schema.
        /// </summary>
        public virtual string UserDeviceId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Type in the schema.
        /// </summary>
        public virtual string Type
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LastLogin in the schema.
        /// </summary>
        public virtual System.DateTime LastLogin
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
