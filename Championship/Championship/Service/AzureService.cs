using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Championship.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Plugin.Connectivity;

namespace Championship.Service
{
    public class AzureService
    {
        public const string BaseUrl = "http://championshipxamarin.azurewebsites.net/";

        public static AzureService Instance { get; } = new AzureService();

		// Client reference.
        public MobileServiceClient DefaultManager { get; private set;  }

        IMobileServiceSyncTable<Bache> bacheTable;

        public AzureService()
        {
        }

        public async Task Initialize()
        {
            DefaultManager = new MobileServiceClient(BaseUrl);

			var path = "baches.db";
			path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

			var store = new MobileServiceSQLiteStore(path);

			store.DefineTable<Bache>();

			await DefaultManager.SyncContext.InitializeAsync(store);
			bacheTable = DefaultManager.GetSyncTable<Bache>();
        }

		public async Task<IEnumerable<Bache>> GetBaches()
		{
			await SyncBaches();

			return await bacheTable.ToEnumerableAsync(); ;

		}

		public async Task SyncBaches()
		{
			try
			{
				if (!CrossConnectivity.Current.IsConnected)
					return;

                await bacheTable.PullAsync("allBache", bacheTable.CreateQuery());

				await DefaultManager.SyncContext.PushAsync();
			}
			catch
			{
			}

		}

        public async Task<bool> AddBache(double latitude, double longitude, Stream photo)
        {
            try
            {
                var imageurl = await App.Authenticator.UploadImage(photo);

                var coffee = new Bache
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    PhotoUrl = imageurl,
                    DateUtc = DateTime.UtcNow,
                };

                await bacheTable.InsertAsync(coffee);

                await SyncBaches();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
