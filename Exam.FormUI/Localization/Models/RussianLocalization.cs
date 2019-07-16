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
