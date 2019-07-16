using Exam.FormUI.Models.ObservableCollections;
using System;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Category model.
    /// </summary>
    [Serializable]
    public class Category
    {
        /// <summary>
        /// Index category, according to the collection
        /// (<see cref="ProductsCategories"/>).
        /// </summary>
        public int Current { get; set; }
    }
}
