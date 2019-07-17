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

using System.Windows;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// A listing required to simplify access to different messages in
    /// different languages.
    /// </summary>
    public enum MessageCategory
    {
        /// <summary>
        /// Message about the removal of the product, directly the "internal"
        /// text of <see cref="MessageBox"/>.
        /// </summary>
        RemoveProductText,

        /// <summary>
        /// (<see cref="RemoveProductText"/>) product removal message header.
        /// </summary>
        RemoveProductCaption
    }
}
