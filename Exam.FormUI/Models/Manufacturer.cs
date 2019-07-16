using Exam.FormUI.Models.ObservableCollections;
using System;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Manufacturer model.
    /// </summary>
    [Serializable]
    public class Manufacturer
    {
        /// <summary>
        /// Producer index, according to the
        /// <see cref="ProductsManufactureries"/> collection.
        /// </summary>
        public int Current { get; set; }
    }
}
