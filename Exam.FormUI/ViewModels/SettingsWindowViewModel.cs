using Exam.FormUI.Models.ObservableCollections;
using Exam.FormUI.Roles;
using Exam.FormUI.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Exam.FormUI.ViewModels
{
    /// <summary>
    /// The view model for the window <see cref="SettingsWindow"/>.
    /// </summary>
    public class SettingsWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The class object responsible for file operations.
        /// </summary>
        private readonly FileOperation fileOperation;

        /// <summary>
        /// <see cref="ComboBox"/> value. Required to compile the correct log
        /// file.
        /// </summary>
        private int selectedLocalization;

        /// <summary>
        /// <see cref="ComboBox"/> value. Required to compile the correct log
        /// file.
        /// </summary>
        private int selectedTheme;

        #endregion

        #region Commands

        private RelayCommand okCommand;
        private RelayCommand cancelCommand;

        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                {
                    okCommand = new RelayCommand(action => Save());
                }
                return okCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(action => Application.Current.MainWindow.Close());
                }
                return cancelCommand;
            }
        }

        /// <summary>
        /// Saves the current settings to a log file and closes the window.
        /// </summary>
        private void Save()
        {
            fileOperation.Settings.Localization = Localizations[selectedLocalization];
            fileOperation.Settings.Theme = Themes[selectedTheme];
            fileOperation.Save();
            Application.Current.MainWindow.Close();
        }

        #endregion

        #region Properties

        /// <summary>
        /// A collection of all available localizations.
        /// </summary>
        public Localizations Localizations { get; set; }

        /// <summary>
        /// A collection of all available themes.
        /// </summary>
        public Models.ObservableCollections.Themes Themes { get; set; }

        /// <summary>
        /// <see cref="ComboBox"/> value. Required to compile the correct log
        /// file.
        /// </summary>
        public int SelectedLocalization
        {
            get => selectedLocalization;

            set
            {
                selectedLocalization = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// <see cref="ComboBox"/> value. Required to compile the correct log
        /// file.
        /// </summary>
        public int SelectedTheme
        {
            get => selectedTheme;

            set
            {
                selectedTheme = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Constructor of the <see cref="SettingsWindow"/> window view model.
        /// </summary>
        public SettingsWindowViewModel()
        {
            // Memory allocation for collections.
            Localizations = new Localizations();
            Themes = new Models.ObservableCollections.Themes();

            // Memory allocation on object for work with files.
            fileOperation = new FileOperation
            {
                SettingsFileName = "Data/settings.dat"
            };

            // Loading log. Log - information about the current topic and
            // localization.
            fileOperation.Load();

            // Selects the values that the user selected last time. If the
            // application is launched for the first time, or the settings.dat
            // file is missing, the default value is zero
            // (significant data type).
            SelectedLocalization = Localizations.IndexOf(fileOperation.Settings.Localization);
            SelectedTheme = Themes.IndexOf(fileOperation.Settings.Theme);
        }

        #region INotifyPropertyChanged Members
        
        /// <summary>
        /// Executed when the property changes.
        /// </summary>
        /// <param name="str">Property name - transmit optional.</param>
        public void OnPropertyChanged([CallerMemberName]string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
