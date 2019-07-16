using System.Collections.ObjectModel;

namespace Exam.FormUI.Models.ObservableCollections
{
    /// <summary>
    /// A collection of all available themes.
    /// </summary>
    public class Themes : ObservableCollection<string>
    {
        public Themes()
        {
            Add("Dark");
            Add("Light");
        }
    }
}
