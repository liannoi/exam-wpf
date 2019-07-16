using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// A collection of all available manufacturers.
    /// </summary>
    public class ProductsManufactureries : ObservableCollection<string>
    {
        public ProductsManufactureries()
        {
            Add("(Nope)");
            Add("Hewlett-Packard");
            Add("Lenovo");
            Add("Bloody");
            Add("Dell");
        }
    }
}
