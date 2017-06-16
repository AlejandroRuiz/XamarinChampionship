// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Championship.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

        #region Setting Constants

        private const string IsLoggedInKey = "settings_key";
        private static readonly bool IsLoggedInDefault = false;

    #endregion


        public static bool IsLoggedIn
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsLoggedInKey, IsLoggedInDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsLoggedInKey, value);
            }
        }

  }
}