  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.UserSubscription, NewsAppModel in the schema.
    /// </summary>
    public partial class UserSubscription {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for UserSubscription constructor in the schema.
        /// </summary>
        public UserSubscription()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for UserSubscriptionId in the schema.
        /// </summary>
        public virtual int UserSubscriptionId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for SubscriptionType in the schema.
        /// </summary>
        public virtual string SubscriptionType
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
