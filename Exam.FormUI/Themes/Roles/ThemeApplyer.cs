using System;
using System.Windows;

namespace Exam.FormUI.Themes.Roles
{
    /// <summary>
    /// Роль обеспечивающая изменение темы приложения.
    /// </summary>
    public static class ThemeApplyer
    {
        /// <summary>
        /// Применяет тему для приложения.
        /// </summary>
        /// <param name="shortName">Краткое именование стиля (Dark, например)</param>
        public static void Apply(string shortName)
        {
            Uri uri = new Uri($"Themes/Dictionaries/{shortName}Theme.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
