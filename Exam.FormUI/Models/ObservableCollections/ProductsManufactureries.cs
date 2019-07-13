using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// Коллекция всех доступных производителей.
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
