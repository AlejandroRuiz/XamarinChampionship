using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Championship.Pages
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
				if (position == null)
                    throw new Exception();
                _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));
			}
			catch
			{
                await DisplayAlert("Error", "Ubicacion no disponible", "Ok");
			}
            try
            {
                _map.Pins.Clear();
                var baches = await Service.AzureService.Instance.GetBaches();
                foreach (var bache in baches)
                {
                    var pin = new Pin
                    {
                        Label = bache.DateDisplay,
                        Address = "Pulsa para ver foto",
                        Position = new Position(bache.Latitude, bache.Longitude),
                        Type = PinType.SavedPin
                    };
                    pin.Clicked += (sender, e) => {
                        Navigation.PushAsync(new BachePhotoPage(bache));
                    };
                    _map.Pins.Add(pin);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Datos no disponibles", "Ok");
            }
        }
    }
}
