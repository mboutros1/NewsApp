using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    public partial class NewsFeed
    {
        public virtual void AddComments(Comment comment)
        {
            Comments.Add(comment);
            comment.NewsFeed = this;
            
        }
    }
}
