using System;
using System.Windows;

namespace Exam.FormUI.Themes.Roles
{
    /// <summary>
    /// The role of changing the theme of the application.
    /// </summary>
    public static class ThemeApplyer
    {
        /// <summary>
        /// Applies a theme for the application.
        /// </summary>
        /// <param name="shortName">Short style name (Dark, for example)</param>
        public static void Apply(string shortName)
        {
            Uri uri = new Uri($"Themes/Dictionaries/{shortName}Theme.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
