// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Fitness_app.Helpers
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

        public static string LoginToken = "";
		private static readonly string SettingsDefault = string.Empty;

		#endregion


        public static string AuthLoginToken
		{
			get
			{
                return AppSettings.GetValueOrDefault(LoginToken, SettingsDefault);
			}
			set
			{
                AppSettings.AddOrUpdateValue(LoginToken, value);
			}
		}
	}
}