using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Championship.Pages
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _logoImage.FadeTo(1, 2000);
        }
    }
}
