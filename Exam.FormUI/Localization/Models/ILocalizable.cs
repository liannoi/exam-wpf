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
