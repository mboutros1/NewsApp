﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    public partial class Comment
    {
        partial void OnCreated()
        {
            this.NewsFeed = new NewsFeed();
            this.User = new User();
        }
    }
}
