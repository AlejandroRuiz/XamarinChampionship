using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Championship.Service;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Gcm.Client;

namespace Championship.Droid
{
    [Activity(Label = "Championship.Droid", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.FormsMaps.Init(this, bundle);

			// Initialize the authenticator before loading the app.
			App.Init((IAuthenticate)this);
            AzureService.Instance.Initialize();
            LoadApplication(new App());
        }

		// Define a authenticated user.
        public MobileServiceUser User { get; private set; }

		public async Task<bool> Authenticate()
		{
			var success = false;
			var message = string.Empty;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
                User = await AzureService.Instance.DefaultManager.LoginAsync(this,
					MobileServiceAuthenticationProvider.Facebook);
				if (User != null)
				{
					success = true;
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			return success;
		}

        public async Task<string> UploadImage(Stream photo)
        {
			var bytes = ReadFully(photo);
			var container = GetContainer();
			string identifier = string.Format("{0}.jpg", Guid.NewGuid().ToString());
			var fileBlob = container.GetBlockBlobReference(identifier);

			await fileBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
            return fileBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }

		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		static CloudBlobContainer GetContainer()
		{
			var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=championshipxamarin;AccountKey=I8tnqP8rLFzvbm6DkLtas+gs5DQ8dGWptfXeMx5yoDmiKt0VTaVN/AbvGtj7j3+M5i8g7zSNXQIawuvVosH+Pg==;EndpointSuffix=core.windows.net");
			var client = account.CreateCloudBlobClient();
			return client.GetContainerReference("baches");
		}
    }
}
