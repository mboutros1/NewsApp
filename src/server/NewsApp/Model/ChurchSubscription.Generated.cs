  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.ChurchSubscription, NewsAppModel in the schema.
    /// </summary>
    public partial class ChurchSubscription {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for ChurchSubscription constructor in the schema.
        /// </summary>
        public ChurchSubscription()
        {
            this.Users = new List<User>();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for ChurchSubscriptionId in the schema.
        /// </summary>
        public virtual int ChurchSubscriptionId
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
        /// There are no comments for IsEvent in the schema.
        /// </summary>
        public virtual bool IsEvent
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Church in the schema.
        /// </summary>
        public virtual Church Church
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
    }

}
