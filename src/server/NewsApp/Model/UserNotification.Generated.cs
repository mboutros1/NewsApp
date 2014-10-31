  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.UserNotification, NewsAppModel in the schema.
    /// </summary>
    public partial class UserNotification {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for UserNotification constructor in the schema.
        /// </summary>
        public UserNotification()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for UserNotificationId in the schema.
        /// </summary>
        public virtual int UserNotificationId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LastSeen in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> LastSeen
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for SendDate in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> SendDate
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

    
        /// <summary>
        /// There are no comments for Notification in the schema.
        /// </summary>
        public virtual Notification Notification
        {
            get;
            set;
        }
    }

}
