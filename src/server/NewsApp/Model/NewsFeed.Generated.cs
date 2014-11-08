﻿  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.NewsFeed, NewsAppModel in the schema.
    /// </summary>
    public partial class NewsFeed {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for NewsFeed constructor in the schema.
        /// </summary>
        public NewsFeed()
        {
            this.UserNotification = new ArrayList();
            this.Comments = new List<Comment>();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for NewsFeedId in the schema.
        /// </summary>
        public virtual int NewsFeedId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Body in the schema.
        /// </summary>
        public virtual string Body
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
        /// There are no comments for CreateDate in the schema.
        /// </summary>
        public virtual System.DateTime CreateDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for SubscriptionType in the schema.
        /// </summary>
        public virtual System.Nullable<int> SubscriptionType
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Likes in the schema.
        /// </summary>
        public virtual int Likes
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Images in the schema.
        /// </summary>
        public virtual string Images
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for ScheduleDate in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> ScheduleDate
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for IsSent in the schema.
        /// </summary>
        public virtual bool IsSent
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CreatedBy in the schema.
        /// </summary>
        public virtual User CreatedBy
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Chruch in the schema.
        /// </summary>
        public virtual Church Chruch
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

    
        /// <summary>
        /// There are no comments for Comments in the schema.
        /// </summary>
        public virtual IList<Comment> Comments
        {
            get;
            set;
        }
    }

}