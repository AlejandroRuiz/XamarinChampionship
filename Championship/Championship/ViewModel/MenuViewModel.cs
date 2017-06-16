using System;
using MvvmHelpers;
using Xamarin.Forms;

namespace Championship.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel()
        {
            Title = "menu";
            Icon = "ic_menu_white";
            SignOutCommand = new Command(InvokeSignOutCommand);
        }

        public Command SignOutCommand { get; }

        async void InvokeSignOutCommand()
        {
            try
            {
                await Service.AzureService.Instance.DefaultManager.LogoutAsync();
            }catch
            {}
            finally{
                App.GoToLoginPage();
            }
        }
    }
}
