using System;
using System.Collections.Generic;
using Championship.Model;
using Xamarin.Forms;

namespace Championship.Pages
{
    public partial class BachePhotoPage : ContentPage
    {
        public BachePhotoPage(Bache bache)
        {
            InitializeComponent();
            _photo.Source = new UriImageSource
            {
                CacheValidity = TimeSpan.FromDays(1),
                CachingEnabled = true,
                Uri = new Uri(bache.PhotoUrl)
            };
        }
    }
}
