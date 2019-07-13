using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// Коллекция всех доступных локализаций.
    /// </summary>
    public class Localizations : ObservableCollection<string>
    {
        public Localizations()
        {
            Add("Russian");
            Add("English");
        }
    }
}
