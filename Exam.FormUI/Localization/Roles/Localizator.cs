// Copyright 2020 Maksym Liannoi
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Exam.FormUI.Localization.Models;
using System;
using System.Windows;

namespace Exam.FormUI.Localization.Roles
{
    /// <summary>
    /// Provides a convenient localization change in the application.
    /// </summary>
    public class Localizator
    {
        /// <summary>
        /// Stores the selected localization.
        /// </summary>
        private ILocalizable localizable;

        /// <summary>
        /// Sets English localization.
        /// </summary>
        public void English()
        {
            Uri uri = new Uri($"Localization/Dictionaries/Dictionary.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            localizable = new EnglishLocalization();
        }

        /// <summary>
        /// Sets Russian localization.
        /// </summary>
        public void Russian()
        {
            Uri uri = new Uri($"Localization/Dictionaries/Dictionary.ru.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            localizable = new RussianLocalization();
        }

        /// <summary>
        /// Returns product main text of the deletion message.
        /// </summary>
        /// <returns>Message</returns>
        public string RemoveProductText()
        {
            return localizable.Messages[(int)MessageCategory.RemoveProductText];
        }

        /// <summary>
        /// Returns product сaption of the deletion message.
        /// </summary>
        /// <returns>Message</returns>
        public string RemoveProductCaption()
        {
            return localizable.Messages[(int)MessageCategory.RemoveProductCaption];
        }
    }
}
