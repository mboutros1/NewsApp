  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.FeedBack, NewsAppModel in the schema.
    /// </summary>
    public partial class FeedBack {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for FeedBack constructor in the schema.
        /// </summary>
        public FeedBack()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for FeedBackId in the schema.
        /// </summary>
        public virtual int FeedBackId
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
        /// There are no comments for CreateDate in the schema.
        /// </summary>
        public virtual System.DateTime CreateDate
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
