using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test6
{
    public class JavascriptChromeWebViewClient : WebChromeClient
    {
        private MainActivity _activity;
        private Uri imageUri;

        public JavascriptChromeWebViewClient(MainActivity activity)
        {
            _activity = activity;
        }

        public override bool OnJsAlert(WebView view, string url, string message, JsResult result)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(_activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("");
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {

            });
            alert.Show();
            result.Confirm();
            return true;
        }

        public override bool OnJsConfirm(WebView view, string url, string message, JsResult result)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(_activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("");
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                result.Confirm();
            });
            alert.SetButton2("Cancel", (c, ev) =>
            {
                result.Cancel();
            });
            alert.Show();
            return true;
        }

        public override bool OnJsPrompt(WebView view, string url, string message, string defaultValue, JsPromptResult result)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(_activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("");
            alert.SetMessage(message);

            EditText et = new EditText(view.Context);
            et.Text = defaultValue;
            alert.SetView(et);

            alert.SetButton("Ok", (c, ev) =>
            {
                result.Confirm(et.Text);
            });
            alert.SetButton2("Cancel", (c, ev) =>
            {
                result.Cancel();
            });
            alert.Show();
            return true;
        }

        public override bool OnConsoleMessage(ConsoleMessage consoleMessage)
        {
            System.Console.WriteLine("Console Message : = " + consoleMessage.Message());

            return base.OnConsoleMessage(consoleMessage);
        }

        public override bool OnShowFileChooser(WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            if (_activity.mUploadMessage != null)
                _activity.mUploadMessage.OnReceiveValue(null);

            _activity.mUploadMessage = filePathCallback;

            try
            {
                File file = createImageFile();
                imageUri = Uri.FromFile(file);

                _activity.mCameraPhotoPath = imageUri;

                Intent captureIntent = new Intent(MediaStore.ActionImageCapture);
                captureIntent.PutExtra(MediaStore.ExtraOutput, imageUri);

                Intent i = new Intent(Intent.ActionGetContent);
                i.AddCategory(Intent.CategoryOpenable);
                i.SetType("image/*");

                Intent chooserIntent = Intent.CreateChooser(i, "이미지 선택기");
                chooserIntent.PutExtra(Intent.ExtraInitialIntents, new IParcelable[] { captureIntent });

                _activity.StartActivityForResult(chooserIntent, MainActivity.FILECHOOSER_RESULTCODE);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return true;
        }

        public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
        {
            base.OnGeolocationPermissionsShowPrompt(origin, callback);

            callback.Invoke(origin, allow: true, retain: false);
        }

        private File createImageFile()
        {
            System.String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Java.Util.Date());
            System.String imageFileName = "JPEG_" + timeStamp + "_";
            File storageDir = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
            File imageFile = File.CreateTempFile(imageFileName, ".jpg", storageDir);

            return imageFile;
        }
    }
}