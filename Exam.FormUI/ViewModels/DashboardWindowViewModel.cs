using Exam.FormUI.Localization.Roles;
using Exam.FormUI.Models;
using Exam.FormUI.Models.ObservableCollections;
using Exam.FormUI.Roles;
using Exam.FormUI.Themes.Roles;
using Exam.FormUI.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Exam.FormUI.ViewModels
{
    /// <summary>
    /// Модель представления для основного окна <see cref="DashboardWindow"/>.
    /// </summary>
    public class DashboardWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Текущий продукт.
        /// </summary>
        private Product product;

        /// <summary>
        /// Объект класса ответственного за операции с файлами.
        /// </summary>
        private readonly FileOperation fileOperation;

        /// <summary>
        /// Объект класса ответственного за локализацию приложения. Данная
        /// форма может влиять на локализацию всего приложения.
        /// </summary>
        private readonly Localizator localizator;

        #endregion

        #region Commands

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand exitCommand;
        private RelayCommand addCommand;
        private RelayCommand removeCommand;
        private RelayCommand optionsCommand;
        private RelayCommand photoCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(action => Products.Clear(), o => !IsProductsZero());
                }
                return newCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(action => Save(), o => !IsProductsZero());
                }
                return saveCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(action => Application.Current.Shutdown());
                }
                return exitCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(action => Add());
                }
                return addCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new RelayCommand(action => RemoveCurrent(), o => !IsProductsZero());
                }
                return removeCommand;
            }
        }

        public ICommand OptionsCommand
        {
            get
            {
                if (optionsCommand == null)
                {
                    optionsCommand = new RelayCommand(action => DisplaySettings());
                }
                return optionsCommand;
            }
        }

        public ICommand PhotoCommand
        {
            get
            {
                if (photoCommand == null)
                {
                    photoCommand = new RelayCommand(action => ShowPhotos(), o => Product != null);
                }
                return photoCommand;
            }
        }

        /// <summary>
        /// Добавляет новый продукт.
        /// </summary>
        private void Add()
        {
            int id;
            try
            {
                id = Products.Max(result => result.Id + 1);
            }
            catch (InvalidOperationException)
            {
                id = 1;
            }
            DefaultProduct(id);
        }

        /// <summary>
        /// Сохранение коллекции (<see cref="Products"/>) в бинарный файл.
        /// </summary>
        private void Save()
        {
            fileOperation.Serialize();
        }

        /// <summary>
        /// Выводит окно с надстройками приложения, после закрытия которого,
        /// приложения еще раз локализуется и устанавливает тему. Это сделано
        /// для обеспечения динамического приложения.
        /// </summary>
        private void DisplaySettings()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
            Apply();
        }

        /// <summary>
        /// Выводит окно с фотографиями текущего продукта.
        /// </summary>
        private void ShowPhotos()
        {
            ReadmoreWindowViewModel viewModel = new ReadmoreWindowViewModel(Products, Products.IndexOf(product), Product.Photos)
            {
                Id = Product.Id
            };
            ReadmoreWindow window = new ReadmoreWindow
            {
                DataContext = viewModel,
                Title = Product.Name
            };
            window.ShowDialog();

            // Обновление коллекции.
            Products = fileOperation.Products;
        }

        /// <summary>
        /// Удаляет текущий выбранный продукт, после вопроса.
        /// </summary>
        private void RemoveCurrent()
        {
            if (MessageBox.Show(localizator.RemoveProductText(), localizator.RemoveProductCaption(), MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Products.Remove(Product);
            }
        }

        /// <summary>
        /// Метод определяющий доступность команды.
        /// </summary>
        private bool IsProductsZero()
        {
            return Products.Count == 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Коллекция продуктов.
        /// </summary>
        public ObservableCollection<Product> Products { get; set; }

        /// <summary>
        /// Коллекция всех доступных категорий.
        /// </summary>
        public ProductsCategories Categories { get; set; }

        /// <summary>
        /// Коллекция всех доступных производителей.
        /// </summary>
        public ProductsManufactureries Manufactureries { get; set; }

        /// <summary>
        /// Текущий продукт.
        /// </summary>
        public Product Product
        {
            get => product;

            set
            {
                product = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Конструктор модели представления основного окна <see cref="DashboardWindow"/>.
        /// </summary>
        public DashboardWindowViewModel()
        {
            // Установка относительных путей к необходимым файлам.
            fileOperation = new FileOperation(Products)
            {
                CollectionFileName = "Data/products.dat",
                SettingsFileName = "Data/settings.dat"
            };

            // Попытка получить с файла коллекцию продуктов.
            Products = fileOperation.TryOpen();

            // Если попытка оказалось неудачной, необходимо создать один
            // "стандартный" продукт, чтобы избежать необработанных исключений.
            if (Products.Count == 0)
            {
                DefaultProduct(1);
            }

            // В качестве текущего продукта, выбирается первый.
            Product = Products.FirstOrDefault();

            // Выделение памяти на коллекции.
            Categories = new ProductsCategories();
            Manufactureries = new ProductsManufactureries();

            // Выделение памяти на объект, необходимого для локализации.
            localizator = new Localizator();

            // Окно применяет необходимые для себя стили и локализацию.
            Apply();
        }

        /// <summary>
        /// Вызывает два метода (<see cref="ApplyTheme"/> и <see cref="Localize"/>),
        /// которые устанавливают для окна необходимые свойства.
        /// </summary>
        private void Apply()
        {
            ApplyTheme();
            Localize();
        }

        /// <summary>
        /// Применяет тему к окну.
        /// </summary>
        private void ApplyTheme()
        {
            fileOperation.Load();
            ThemeApplyer.Apply(fileOperation.Settings.Theme);
        }

        /// <summary>
        /// Локализирует окно.
        /// </summary>
        private void Localize()
        {
            fileOperation.Load();
            switch (fileOperation.Settings.Localization)
            {
                case "English":
                    {
                        localizator.English();
                        break;
                    }
                case "Russian":
                    {
                        localizator.Russian();
                        break;
                    }
            }
        }

        /// <summary>
        /// Создает и добавляет обычный продукт. Обычным продуктом, называется
        /// продукт, которые является шаблоном для заполнения пользователем.
        /// </summary>
        /// <param name="id">Код товара</param>
        private void DefaultProduct(int id)
        {
            Products.Add(new Product
            {
                Id = id,
                Name = "Product",
                Category = new Category
                {
                    Current = 0
                },
                Manufacturer = new Manufacturer
                {
                    Current = 0
                }
            });
            Product = Products[Products.Count - 1];
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
