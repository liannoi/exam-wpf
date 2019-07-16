using System;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Settings model.
    /// </summary>
    [Serializable]
    public class Settings
    {
        /// <summary>
        /// Stores the current localization.
        /// </summary>
        public string Localization { get; set; }

        /// <summary>
        /// Stores the current theme.
        /// </summary>
        public string Theme { get; set; }
    }
}
