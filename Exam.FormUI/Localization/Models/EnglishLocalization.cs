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
