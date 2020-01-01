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
    /// Russian localization model. Implements the <see cref="ILocalizable"/>
    /// interface to implement the design pattern Strategy.
    /// </summary>
    public class RussianLocalization : ILocalizable
    {
        /// <summary>
        /// Stores the list of messages in Russian. In parallel, a
        /// <see cref="MessageCategory"/> enumeration will be introduced to
        /// more easily access the message.
        /// </summary>
        List<string> ILocalizable.Messages
        {
            get
            {
                return new List<string>
                {
                    "Вы действительно хотите удалить этот продукт? Вы не сможете его восстановить.",
                    "Удаление продукта"
                };
            }
        }
    }
}
