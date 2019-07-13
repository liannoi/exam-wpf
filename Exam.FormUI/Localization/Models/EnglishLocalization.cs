using System.Collections.Generic;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// Модель английской локализации. Реализует интерфейс
    /// <see cref="ILocalizable"/>, для реализации шаблона проектирования
    /// Cтратегия.
    /// </summary>
    public class EnglishLocalization : ILocalizable
    {
        /// <summary>
        /// Хранит список сообщений на английском языке. Параллельно введется
        /// перечисление <see cref="MessageCategory"/> для более удобного
        /// получения доступа к сообщению.
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
