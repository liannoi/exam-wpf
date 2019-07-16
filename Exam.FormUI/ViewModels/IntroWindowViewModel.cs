using Exam.FormUI.Localization.Roles;
using Exam.FormUI.Models;
using Exam.FormUI.Models.ObservableCollections;
using Exam.FormUI.Roles;
using Exam.FormUI.Views;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Exam.FormUI.ViewModels
{
    /// <summary>
    /// The view model for the window <see cref="IntroWindow"/>.
    /// </summary>
    public class IntroWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The class object responsible for file operations.
        /// </summary>
        private FileOperation fileOperation;

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

        /// <summary>
        /// The class object responsible for the localization of the
        /// application. This form can affect the localization of the entire
        /// application.
        /// </summary>
        private readonly Localizator localizator;

        #endregion

        #region Commands

        private RelayCommand okCommand;

        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                {
                    okCommand = new RelayCommand(action => SaveSettings());
                }
                return okCommand;
            }
        }

        /// <summary>
        /// Prepares data and sends <see cref="fileOperation"/> to save it to a
        /// file.
        /// </summary>
        private void SaveSettings()
        {
            fileOperation = new FileOperation
            {
                Settings = new Settings
                {
                    Localization = Localizations[selectedLocalization],
                    Theme = Themes[selectedTheme]
                },
                SettingsFileName = "Data/settings.dat"
            };
            fileOperation.Save();
            FileCreated();
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

        #region Methods

        /// <summary>
        /// Constructor of the <see cref="IntroWindow"/> window view model.
        /// </summary>
        public IntroWindowViewModel()
        {
            Localizations = new Localizations();
            Themes = new Models.ObservableCollections.Themes();
            if (File.Exists("Data/settings.dat"))
            {
                FileCreated();
            }
            localizator = new Localizator();
        }

        /// <summary>
        /// Describes the actions that need to be performed in case the
        /// settings file has already been created.
        /// </summary>
        private void FileCreated()
        {
            OpenDashboard();
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Describes the opening actions of the main form.
        /// </summary>
        private void OpenDashboard()
        {
            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
        }

        #endregion

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
