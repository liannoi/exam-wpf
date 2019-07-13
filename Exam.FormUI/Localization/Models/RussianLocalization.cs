using System.Collections.Generic;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// Модель русской локализации. Реализует интерфейс
    /// <see cref="ILocalizable"/>, для реализации шаблона проектирования
    /// Cтратегия.
    /// </summary>
    public class RussianLocalization : ILocalizable
    {
        /// <summary>
        /// Хранит список сообщений на русском языке. Параллельно введется
        /// перечисление <see cref="MessageCategory"/> для более удобного
        /// получения доступа к сообщению.
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
