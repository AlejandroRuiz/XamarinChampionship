using System;
using Championship.Service;
using MvvmHelpers;
using Plugin.Geolocator;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Championship.ViewModel
{
    public class CreateNewItemViewModel : BaseViewModel
    {
        public CreateNewItemViewModel()
        {
            Title = "Crear Nuevo Registro";
            TakePhotoCommand = new Command(InvokeTakePhotoCommand);
            SaveCommand = new Command(InvokeSaveCommand);
        }

        MediaFile Photo; 

        public Command TakePhotoCommand { get; }

        public Command SaveCommand { get; }

        async void InvokeTakePhotoCommand()
        {
			var config = new StoreCameraMediaOptions { DefaultCamera = CameraDevice.Front, PhotoSize = PhotoSize.Medium };
			Photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(config);
            OnPropertyChanged(nameof(HasPhoto));
            OnPropertyChanged(nameof(Path));
        }

        public string Path => Photo?.Path;

        public bool HasPhoto => Photo != null;

		async void InvokeSaveCommand()
		{
            IsBusy = true;
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;

				var position = await locator.GetPositionAsync((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
				if (position == null)
					throw new Exception();

                using (var stream = Photo.GetStream())
                {
                    var result = await AzureService.Instance.AddBache(position.Latitude, position.Longitude, stream);
                    if(result)
                    {
                        await App.MasterPage.Detail.Navigation.PopAsync();
                    }
                }
			}
			catch
			{
                await App.Current.MainPage.DisplayAlert("Error", "Ubicacion no disponible", "Ok");
			}
            IsBusy = false;
		}
    }
}
