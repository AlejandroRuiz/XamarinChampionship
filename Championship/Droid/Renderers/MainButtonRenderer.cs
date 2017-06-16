using System;
using Championship.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(MainButtonRenderer))]
namespace Championship.Droid.Renderers
{
    public class MainButtonRenderer : ButtonRenderer
    {
        public MainButtonRenderer()
        {
        }
    }
}
