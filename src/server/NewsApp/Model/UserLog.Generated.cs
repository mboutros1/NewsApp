  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.UserLog, NewsAppModel in the schema.
    /// </summary>
    public partial class UserLog {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for UserLog constructor in the schema.
        /// </summary>
        public UserLog()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for UserLogId in the schema.
        /// </summary>
        public virtual int UserLogId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LogTime in the schema.
        /// </summary>
        public virtual System.DateTime LogTime
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LogType in the schema.
        /// </summary>
        public virtual LogType LogType
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
        /// There are no comments for NewsFeed in the schema.
        /// </summary>
        public virtual NewsFeed NewsFeed
        {
            get;
            set;
        }
    }

}
