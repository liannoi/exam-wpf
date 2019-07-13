using Exam.FormUI.Models.ObservableCollections;
using System;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Модель производителя.
    /// </summary>
    [Serializable]
    public class Manufacturer
    {
        /// <summary>
        /// Индекс производителя, согласно коллекции (<see cref="ProductsManufactureries"/>).
        /// </summary>
        public int Current { get; set; }
    }
}
