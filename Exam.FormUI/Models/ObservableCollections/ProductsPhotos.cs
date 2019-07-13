using System;
using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// Модель коллекции фотографий, для каждого продукта.
    /// </summary>
    [Serializable]
    public class ProductsPhotos : ObservableCollection<Photo>
    {
    }
}
