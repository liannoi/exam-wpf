using System.Collections.Generic;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// Интерфейс необходимый для реализации шаблона проектирования Стратегия.
    /// Все классы исполняющие роль модели для локализации, должны
    /// реализовывать этот интерфейс.
    /// </summary>
    public interface ILocalizable
    {
        /// <summary>
        /// Хранит список сообщений, в которых вы можете записать любое
        /// сообщение и на любом языке. Для удобства дальнейшего получения
        /// значения, введется перечисление <see cref="MessageCategory"/>.
        /// </summary>
        List<string> Messages { get; }
    }
}
