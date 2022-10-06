
using System;
using System.IO;
using AppSIMyS.Droid;
using AppSIMyS;
using Android.Content;
using AppSIMyS.ViewModels;
using Java.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Support.V4;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using AndroidX.Core.Content;
using AndroidX.Core.App;

[assembly: Dependency(typeof(SaveAndroid))]
class SaveAndroid : ISavePdf
{
    //Method to save document as a file in Android and view the saved document
    public  async Task SaveAndView(string fileName, String contentType, MemoryStream stream, string root)
    {
        //string root = null;

        if (ContextCompat.CheckSelfPermission(Forms.Context, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
        {
            ActivityCompat.RequestPermissions((Android.App.Activity)Forms.Context, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
        }
        Java.IO.File myDir = new Java.IO.File(root);
        myDir.Mkdir();

        Java.IO.File file = new Java.IO.File(myDir, fileName);

        //Remove if the file exists
        if (file.Exists()) file.Delete();

        //Write the stream into the file
        FileOutputStream outs = new FileOutputStream(file);
        outs.Write(stream.ToArray());

        outs.Flush();
        outs.Close();

        //Invoke the created file for viewing
        if (file.Exists())
        {
            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
            string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".provider", file);
            intent.SetDataAndType(path, mimeType);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
        }

        
    }
}
    
