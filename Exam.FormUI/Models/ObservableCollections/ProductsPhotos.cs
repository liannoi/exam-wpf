using System;
using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// Model collection of photos for each product.
    /// </summary>
    [Serializable]
    public class ProductsPhotos : ObservableCollection<Photo>
    {
    }
}
