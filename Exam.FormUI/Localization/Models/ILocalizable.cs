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

using System.Collections.Generic;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// The interface required to implement the design pattern Strategy. All
    /// classes are performing a role model for localization must implement
    /// this interface.
    /// </summary>
    public interface ILocalizable
    {
        /// <summary>
        /// Stores a list of messages in which your can record any message in
        /// any language. For the convenience of further retrieving the value,
        /// a <see cref="MessageCategory"/> enumeration is introduced.
        /// </summary>
        List<string> Messages { get; }
    }
}
