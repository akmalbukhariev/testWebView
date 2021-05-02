using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test6
{
    public class JavascriptWebViewClient : WebViewClient
    {
        private MainActivity _activity;
        public JavascriptWebViewClient(MainActivity activity)
        {
            _activity = activity;
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);
        }

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            if (url.StartsWith("tel:"))
            {
                string strPhone = url.Replace("tel:", "");
                Xamarin.Essentials.PhoneDialer.Open(strPhone);
                return true;
            }
            else
            {
                view.LoadUrl(url);
            }

            #region
            // if (url == "https://dang.webadsky.net/skin/map.php")
            //{
            //    Uri uri = Uri.Parse(url);
            //    Intent intent = new Intent(Intent.ActionView, uri);
            //    intent.AddCategory(Intent.CategoryBrowsable);
            //    intent.PutExtra(Browser.ExtraApplicationId, Xamarin.Essentials.AppInfo.PackageName);

            //    try
            //    {
            //        _activity.StartActivity(intent);
            //        return true;
            //    }
            //    catch (ActivityNotFoundException e)
            //    {
            //        return false;
            //    }
            //}
            //else
            //if (url.StartsWith("http:") || url.StartsWith("https:"))
            //{
            //    return false;
            //}
            //else if (url.StartsWith("tel:"))
            //{
            //    string strPhone = url.Replace("tel:", "");
            //    Xamarin.Essentials.PhoneDialer.Open(strPhone);
            //    return true;
            //}
            //else
            //{
            //    Intent intent = null;
            //    try
            //    {
            //        intent = Intent.ParseUri(url, Android.Content.IntentUriType.Scheme);
            //        System.Console.WriteLine("Url: " + url);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        System.Console.WriteLine("[error] Bad request uri format : [" + url + "] = " + ex.Message);
            //        return false;
            //    }

            //    if (_activity.PackageManager.ResolveActivity(intent, 0) == null)
            //    {
            //        System.String pkgName = intent.Package;
            //        if (pkgName != null)
            //        {
            //            Uri uri = Uri.Parse("market://search?q=pname:" + pkgName);
            //            intent = new Intent(Intent.ActionView, uri);
            //            _activity.StartActivity(intent);
            //        }
            //    }
            //    else
            //    {
            //        Uri uri = Uri.Parse(intent.DataString);
            //        intent = new Intent(Intent.ActionView, uri);
            //        _activity.StartActivity(intent);
            //        System.Console.WriteLine("Intent " + intent.DataString);
            //    }
            //}
            #endregion

            return true;
        }

        public override void OnLoadResource(WebView view, string url)
        {
            //System.Console.WriteLine("Progress: ========" + view.Progress.ToString());
        }
    }
}