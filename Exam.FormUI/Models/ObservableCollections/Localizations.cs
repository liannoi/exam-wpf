using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// A collection of all available localizations.
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
