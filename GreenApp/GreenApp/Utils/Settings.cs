﻿// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GreenApp.Utils
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

    private const string LastEmailSettings = "last_email_key";
    private static readonly string SettingsDefault = string.Empty;

    #endregion


    public static string LastUsedEmail
    {
        get
        {
            return AppSettings.GetValueOrDefault(LastEmailSettings, SettingsDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(LastEmailSettings, value);
        }
    }

}
}