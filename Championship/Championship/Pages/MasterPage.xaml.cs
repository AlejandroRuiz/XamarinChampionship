using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Championship.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            SetDetailPage(new MapPage());
        }

        public void SetDetailPage(Page page)
        {
            if (!(page is NavigationPage))
                page = new NavigationPage(page) { BarTextColor = Color.White, BarBackgroundColor = Color.FromHex("#FF5722") };
            Detail = page;
        }
    }
}
