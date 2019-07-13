using System;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Модель настроек.
    /// </summary>
    [Serializable]
    public class Settings
    {
        /// <summary>
        /// Хранит текущею локализацию.
        /// </summary>
        public string Localization { get; set; }

        /// <summary>
        /// Хранит текущею тему.
        /// </summary>
        public string Theme { get; set; }
    }
}
