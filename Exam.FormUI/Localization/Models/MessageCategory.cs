using System.Windows;

namespace Exam.FormUI.Localization.Models
{
    /// <summary>
    /// A listing required to simplify access to different messages in
    /// different languages.
    /// </summary>
    public enum MessageCategory
    {
        /// <summary>
        /// Message about the removal of the product, directly the "internal"
        /// text of <see cref="MessageBox"/>.
        /// </summary>
        RemoveProductText,

        /// <summary>
        /// (<see cref="RemoveProductText"/>) product removal message header.
        /// </summary>
        RemoveProductCaption
    }
}
