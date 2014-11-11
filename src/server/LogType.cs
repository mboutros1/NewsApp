  
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    /// <summary>
    /// There are no comments for NewsApp.Model.LogType, NewsAppModel in the schema.
    /// </summary>
    public enum LogType : int
 {
    
        /// <summary>
        /// There are no comments for LogType.Post in the schema.
        /// </summary>
        Post = 0,    
        /// <summary>
        /// There are no comments for LogType.Comment in the schema.
        /// </summary>
        Comment = 1,    
        /// <summary>
        /// There are no comments for LogType.Like in the schema.
        /// </summary>
        Like = 2,    
        /// <summary>
        /// There are no comments for LogType.FeedBack in the schema.
        /// </summary>
        FeedBack = 3,    
        /// <summary>
        /// There are no comments for LogType.ChangeSubscription in the schema.
        /// </summary>
        ChangeSubscription = 4
    }

}
