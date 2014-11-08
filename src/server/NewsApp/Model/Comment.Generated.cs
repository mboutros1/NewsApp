  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.Comment, NewsAppModel in the schema.
    /// </summary>
    public partial class Comment {
    
        #region Extensibility Method Definitions
        
        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();
        
        #endregion
        /// <summary>
        /// There are no comments for Comment constructor in the schema.
        /// </summary>
        public Comment()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for CommentId in the schema.
        /// </summary>
        public virtual int CommentId
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
        /// There are no comments for Images in the schema.
        /// </summary>
        public virtual string Images
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
