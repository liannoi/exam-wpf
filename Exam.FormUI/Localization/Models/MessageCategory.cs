using System.Windows;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// Перечисление, необходимое для упрощения получения доступа к разным
    /// сообщением, на разных языках.
    /// </summary>
    public enum MessageCategory
    {
        /// <summary>
        /// Сообщение об удаление продукта, непосредственно "внутренний" текст
        /// <see cref="MessageBox"/>.
        /// </summary>
        RemoveProductText,

        /// <summary>
        /// Заголовок сообщения об удаление продукта (<see cref="RemoveProductText"/>).
        /// </summary>
        RemoveProductCaption
    }
}
