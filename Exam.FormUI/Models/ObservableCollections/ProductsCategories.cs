using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// A collection of all available categories.
    /// </summary>
    public class ProductsCategories : ObservableCollection<string>
    {
        public ProductsCategories()
        {
            Add("(Nope)");
            Add("Принтеры");
            Add("Системные блоки");
            Add("Компьютерные мыши");
        }
    }
}
