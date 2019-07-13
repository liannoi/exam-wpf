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
    /// Модель представления для окна <see cref="SettingsWindow"/>.
    /// </summary>
    public class SettingsWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Объект класса ответственного за операции с файлами.
        /// </summary>
        private readonly FileOperation fileOperation;

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
        /// Сохраняет текущие настройки в лог файл и закрывает окно.
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

        /// <summary>
        /// Конструктор модели представления окна <see cref="SettingsWindow"/>.
        /// </summary>
        public SettingsWindowViewModel()
        {
            // Выделение памяти на коллекции.
            Localizations = new Localizations();
            Themes = new Models.ObservableCollections.Themes();

            // Выделение памяти на объект для работы с файлами.
            fileOperation = new FileOperation
            {
                SettingsFileName = "Data/settings.dat"
            };

            // Загрузка лога. Лог — информация о текущей теме и локализации.
            fileOperation.Load();

            // Выбирает те значения, которые выбрал пользователь в прошлый раз.
            // Если запуск приложения в первый раз, или отсутствует файл
            // settings.dat, значение по умолчанию ноль (значимый тип данных).
            SelectedLocalization = Localizations.IndexOf(fileOperation.Settings.Localization);
            SelectedTheme = Themes.IndexOf(fileOperation.Settings.Theme);
        }

        #region INotifyPropertyChanged Members

        public void OnPropertyChanged([CallerMemberName]string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
