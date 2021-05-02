using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace test6
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private WebView web_view;
        public IValueCallback mUploadMessage;
        public Android.Net.Uri mCameraPhotoPath;
        public System.String StrmCameraPhotoPath;
        public static int FILECHOOSER_RESULTCODE = 1;
        private static int INPUT_FILE_REQUEST_CODE = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            SetContentView(Resource.Layout.activity_main);

            web_view = FindViewById<WebView>(Resource.Id.webViewMain);

            web_view.Settings.JavaScriptEnabled = true;
            web_view.Settings.SetGeolocationEnabled(true);
            web_view.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            web_view.Settings.DomStorageEnabled = true;
            web_view.Settings.SetSupportMultipleWindows(true);

            web_view.SetWebViewClient(new JavascriptWebViewClient(this));
            web_view.SetWebChromeClient(new JavascriptChromeWebViewClient(this));
            web_view.AddJavascriptInterface(new WebInterface(), "webApp"); //Ex. var str1 = webApp.getDeviceName(), webApp.setUserId("user1")

            web_view.LoadUrl("https://about.google/contact-google/?hl=en_US&_ga=2.266960270.1211033619.1618712075-1933875531.1618712075");
        }

        public override void OnBackPressed()
        {
            if (web_view.CanGoBack())
            {
                web_view.GoBack();
                return;
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Exit");
                alert.SetMessage("Do you want to exit?");
                alert.SetButton("ok", (c, ev) =>
                {
                    base.OnBackPressed();
                });
                alert.SetButton2("Cancel", (c, ev) =>
                {

                });
                alert.Show();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == FILECHOOSER_RESULTCODE)
            {
                if (null == this.mUploadMessage)
                {
                    return;
                }

                Android.Net.Uri[] result = null;

                try
                {
                    if (resultCode != Result.Ok)
                    {
                        result = null;
                    }
                    else
                    {
                        //Retrieve from the private variable if the intent is null
                        result = data == null ? new Android.Net.Uri[] { mCameraPhotoPath } : new Android.Net.Uri[] { data.Data };
                    }
                }
                catch (System.Exception e)
                {
                    Toast.MakeText(ApplicationContext, "activity :" + e, ToastLength.Long).Show();
                }

                mUploadMessage.OnReceiveValue(result);
                mUploadMessage = null;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}