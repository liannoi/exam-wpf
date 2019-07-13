using Exam.FormUI.Localization.Models;
using System;
using System.Windows;

namespace Exam.FormUI.Localization.Roles
{
    public class Localizator
    {
        private ILocalizable localizable;

        public void English()
        {
            Uri uri = new Uri($"Localization/Dictionaries/Dictionary.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            localizable = new EnglishLocalization();
        }

        public void Russian()
        {
            Uri uri = new Uri($"Localization/Dictionaries/Dictionary.ru.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            localizable = new RussianLocalization();
        }

        public string RemoveProductText()
        {
            return localizable.Messages[(int)MessageCategory.RemoveProductText];
        }

        public string RemoveProductCaption()
        {
            return localizable.Messages[(int)MessageCategory.RemoveProductCaption];
        }
    }
}
