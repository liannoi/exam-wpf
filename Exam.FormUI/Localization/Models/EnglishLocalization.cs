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

using System.Collections.Generic;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// English localization model. Implements an interface
    /// <see cref="ILocalizable"/>, to implement the design pattern Strategy.
    /// </summary>
    public class EnglishLocalization : ILocalizable
    {
        /// <summary>
        /// Stores the list of messages in English. At the same time, an
        /// enumeration (<see cref="MessageCategory"/>) will be introduced for
        /// easier access to the message.
        /// </summary>
        List<string> ILocalizable.Messages
        {
            get
            {
                return new List<string>
                {
                    "Do your really want to delete this product? Your can not restore it.",
                    "Product removal"
                };
            }
        }
    }
}
