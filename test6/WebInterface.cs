using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test6
{
    public class WebInterface : Java.Lang.Object
    {
        public WebInterface()
        {

        }

        [Export("getDeviceName")]
        [JavascriptInterface]
        public String getDeviceName()
        {
            return "";
        }

        [Export("setUserId")]
        [JavascriptInterface]
        public void setUserId(string userId)
        {
           
        }
    }
}