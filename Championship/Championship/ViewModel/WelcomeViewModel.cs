using System;
using System.Threading.Tasks;
using Championship.Helpers;
using Championship.Pages;
using Emotions;
using MvvmHelpers;
using Plugin.Connectivity;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Championship.ViewModel
{
    public class WelcomeViewModel : BaseViewModel
    {
        public WelcomeViewModel()
        {
            FacebookCommand = new Command(InvokeFacebookCommand);
        }

        public Command FacebookCommand { get; }

        public async void InvokeFacebookCommand()
        {
            IsBusy = true;
            if (!CrossConnectivity.Current.IsConnected)
            {
                await App.Current.MainPage.DisplayAlert("Bache App", "Sin Internet :(", "Ok");
            }
            else
            {
                if (await LoginFB())
                {
                    var result = await App.Current.MainPage.DisplayAlert("Bache App", "Alto Humano Demuestra que no eres un robot", "Ok", "Cancel");
                    if(result)
                    {
						try
						{
                            var config = new StoreCameraMediaOptions { DefaultCamera = CameraDevice.Front, PhotoSize = PhotoSize.Medium };
                            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(config);
                            using (var steam = photo.GetStream())
                            {
                                var emotions = await ServiceEmotions.GetEmotions(photo.GetStream());
                                if (emotions != null)
                                {
                                    var message = "Bienvenido Humano" + Environment.NewLine;
									foreach (var item in emotions)
									{
                                        message += item.Key + " : " + item.Value + Environment.NewLine;
									}
                                    await App.Current.MainPage.DisplayAlert("Bache App", message, "Ok");
                                    App.GoToMainPage();
                                    Settings.IsLoggedIn = true;
                                }
                            }
						}
						catch
						{
                            await App.Current.MainPage.DisplayAlert("Bache App", "Atras Robot", "Ok", "Cancel");
						}
                    }
                }
            }
            IsBusy = false;
        }

        async Task<bool> LoginFB()
        {
            return await App.Authenticator.Authenticate();
        }
    }
}
