// Copyright 2019 Maksym Liannoi
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
