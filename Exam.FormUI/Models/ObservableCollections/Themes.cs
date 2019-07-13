using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    // Коллекция всех доступных тем.
    public class Themes : ObservableCollection<string>
    {
        public Themes()
        {
            Add("Dark");
            Add("Light");
        }
    }
}
