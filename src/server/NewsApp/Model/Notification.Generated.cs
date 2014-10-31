  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.Notification, NewsAppModel in the schema.
    /// </summary>
    public partial class Notification {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for Notification constructor in the schema.
        /// </summary>
        public Notification()
        {
            this.UserNotification = new ArrayList();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for NotificationId in the schema.
        /// </summary>
        public virtual int NotificationId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Title in the schema.
        /// </summary>
        public virtual string Title
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Details in the schema.
        /// </summary>
        public virtual string Details
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CreateDate in the schema.
        /// </summary>
        public virtual System.DateTime CreateDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Type in the schema.
        /// </summary>
        public virtual int Type
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for UserNotification in the schema.
        /// </summary>
        public virtual IList UserNotification
        {
            get;
            set;
        }
    }

}
