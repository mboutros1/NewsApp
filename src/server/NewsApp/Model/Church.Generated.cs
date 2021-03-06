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
    /// There are no comments for NewsApp.Model.Church, NewsAppModel in the schema.
    /// </summary>
    public partial class Church {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for Church constructor in the schema.
        /// </summary>
        public Church()
        {
            this.Country = @"US";
            this.HomeUsers = new List<User>();
            this.Users = new List<User>();
            this.Feeds = new List<NewsFeed>();
            this.ChurchSubscriptions = new List<ChurchSubscription>();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for ChurchId in the schema.
        /// </summary>
        public virtual int ChurchId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for DisplayName in the schema.
        /// </summary>
        public virtual string DisplayName
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Address in the schema.
        /// </summary>
        public virtual string Address
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for ZipCode in the schema.
        /// </summary>
        public virtual string ZipCode
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for State in the schema.
        /// </summary>
        public virtual string State
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for City in the schema.
        /// </summary>
        public virtual string City
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Country in the schema.
        /// </summary>
        public virtual string Country
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Latitude in the schema.
        /// </summary>
        public virtual System.Nullable<long> Latitude
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Longitude in the schema.
        /// </summary>
        public virtual System.Nullable<long> Longitude
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for LiveStremUrl in the schema.
        /// </summary>
        public virtual string LiveStremUrl
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for HomeUsers in the schema.
        /// </summary>
        public virtual IList<User> HomeUsers
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Users in the schema.
        /// </summary>
        public virtual IList<User> Users
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Feeds in the schema.
        /// </summary>
        public virtual IList<NewsFeed> Feeds
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for ChurchSubscriptions in the schema.
        /// </summary>
        public virtual IList<ChurchSubscription> ChurchSubscriptions
        {
            get;
            set;
        }
    }

}
