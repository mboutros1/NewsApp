using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NewsApp.Model
{

    public partial class UserDevice
    {
        public override string ToString()
        {
            return UserDeviceId + " " + Type;
        }
    }
}
