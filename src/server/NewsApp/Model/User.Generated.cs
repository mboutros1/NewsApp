  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.User, NewsAppModel in the schema.
    /// </summary>
    public partial class User {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for User constructor in the schema.
        /// </summary>
        public User()
        {
            this.IsAnonymous = true;
            this.Subscriptions = new List<ChurchSubscription>();
            this.Churches = new List<Church>();
            this.Notifications = new List<UserNotification>();
            this.Comments = new List<Comment>();
            this.CreatedNewsFeeds = new List<NewsFeed>();
            this.Devices = new List<UserDevice>();
            this.Roles = new List<UserRole>();
            this.LikedNewsFeeds = new List<NewsFeed>();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for UserId in the schema.
        /// </summary>
        public virtual int UserId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Email in the schema.
        /// </summary>
        public virtual string Email
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
        /// There are no comments for LastModified in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> LastModified
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Avatar in the schema.
        /// </summary>
        public virtual string Avatar
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for BirthDay in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> BirthDay
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for FacebookId in the schema.
        /// </summary>
        public virtual System.Nullable<long> FacebookId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for IsAnonymous in the schema.
        /// </summary>
        public virtual bool IsAnonymous
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for HomeChurch in the schema.
        /// </summary>
        public virtual Church HomeChurch
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Subscriptions in the schema.
        /// </summary>
        public virtual IList<ChurchSubscription> Subscriptions
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Churches in the schema.
        /// </summary>
        public virtual IList<Church> Churches
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Notifications in the schema.
        /// </summary>
        public virtual IList<UserNotification> Notifications
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

    
        /// <summary>
        /// There are no comments for CreatedNewsFeeds in the schema.
        /// </summary>
        public virtual IList<NewsFeed> CreatedNewsFeeds
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Devices in the schema.
        /// </summary>
        public virtual IList<UserDevice> Devices
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Roles in the schema.
        /// </summary>
        public virtual IList<UserRole> Roles
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LikedNewsFeeds in the schema.
        /// </summary>
        public virtual IList<NewsFeed> LikedNewsFeeds
        {
            get;
            set;
        }
    }

}
