using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// Коллекция всех доступных категорий.
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
