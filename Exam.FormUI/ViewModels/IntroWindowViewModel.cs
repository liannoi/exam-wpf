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
    /// Модель представления для окна <see cref="IntroWindow"/>.
    /// </summary>
    public class IntroWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Объект класса ответственного за операции с файлами.
        /// </summary>
        private FileOperation fileOperation;

        /// <summary>
        /// Значение <see cref="ComboBox"/>. Необходимо для составления
        /// корректного лог файла.
        /// </summary>
        private int selectedLocalization;

        /// <summary>
        /// Значение <see cref="ComboBox"/>. Необходимо для составления
        /// корректного лог файла.
        /// </summary>
        private int selectedTheme;

        /// <summary>
        /// Объект класса ответственного за локализацию приложения. Данная
        /// форма может влиять на локализацию всего приложения.
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
        /// Подготавливает данные и отправляет <see cref="fileOperation"/> для
        /// сохранения их в файл.
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
        /// Коллекция всех доступных локализаций.
        /// </summary>
        public Localizations Localizations { get; set; }

        /// <summary>
        /// Коллекция всех доступных тем.
        /// </summary>
        public Models.ObservableCollections.Themes Themes { get; set; }

        /// <summary>
        /// Значение <see cref="ComboBox"/>. Необходимо для составления
        /// корректного лог файла.
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
        /// Значение <see cref="ComboBox"/>. Необходимо для составления
        /// корректного лог файла.
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
        /// Конструктор модели представления окна <see cref="IntroWindow"/>.
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
        /// Описывает действия которые необходимо выполнить, в случае если файл
        /// с настройками уже создан.
        /// </summary>
        private void FileCreated()
        {
            OpenDashboard();
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Описывает действия открытия основной формы.
        /// </summary>
        private void OpenDashboard()
        {
            DashboardWindow dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public void OnPropertyChanged([CallerMemberName]string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
