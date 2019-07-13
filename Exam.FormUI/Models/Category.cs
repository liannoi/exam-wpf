using Exam.FormUI.Models.ObservableCollections;
using System;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Модель категории.
    /// </summary>
    [Serializable]
    public class Category
    {
        /// <summary>
        /// Индекс категории, согласно коллекции (<see cref="ProductsCategories"/>).
        /// </summary>
        public int Current { get; set; }
    }
}
