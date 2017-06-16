using Championship.Helpers;
using Championship.Pages;
using Championship.Service;
using Xamarin.Forms;

namespace Championship
{
    public partial class App : Application
    {
        public static MasterPage MasterPage { get; private set; }

        public App()
        {
            InitializeComponent();

            if (!Settings.IsLoggedIn)
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                MainPage = MasterPage = new MasterPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void GoToMainPage()
        {
            Current.MainPage = MasterPage = new MasterPage();
        }

		public static void GoToLoginPage()
		{
            MasterPage = null;
            Current.MainPage = new NavigationPage(new WelcomePage());
		}

		public static IAuthenticate Authenticator { get; private set; }

		public static void Init(IAuthenticate authenticator)
		{
			Authenticator = authenticator;
		}
    }
}
