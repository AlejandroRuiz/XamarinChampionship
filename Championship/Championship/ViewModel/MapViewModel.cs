using System;
using Championship.Pages;
using MvvmHelpers;
using Xamarin.Forms;

namespace Championship.ViewModel
{
    public class MapViewModel : BaseViewModel
    {
        public MapViewModel()
        {
            Title = "Bache App";
            CreateNewBache = new Command(InvokeCreateNewBache);
        }

        public Command CreateNewBache { get; }

        void InvokeCreateNewBache()
        {
            App.MasterPage.Detail.Navigation.PushAsync(new CreateNewItem());
        }
    }
}
