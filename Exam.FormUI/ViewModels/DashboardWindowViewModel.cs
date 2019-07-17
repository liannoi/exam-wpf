// Copyright 2019 Maksym Liannoi
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
    /// View model for the main window (<see cref="DashboardWindow"/>).
    /// </summary>
    public class DashboardWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Current product.
        /// </summary>
        private Product product;

        /// <summary>
        /// The class object responsible for file operations.
        /// </summary>
        private readonly FileOperation fileOperation;

        /// <summary>
        /// The class object responsible for the localization of the
        /// application. This form can affect the localization of the entire
        /// application.
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
        /// Adds a new product.
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
        /// Save the collection (<see cref="Products"/>) to a binary file.
        /// </summary>
        private void Save()
        {
            fileOperation.Serialize();
        }

        /// <summary>
        /// Displays a window with the add-ins of the application, after which
        /// it is closed, the application is localized again and installs the
        /// theme. This is done to provide a dynamic application.
        /// </summary>
        private void DisplaySettings()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
            Apply();
        }

        /// <summary>
        /// Displays a window with photos of the current product.
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

            // Update collection.
            Products = fileOperation.Products;
        }

        /// <summary>
        /// Removes the currently selected product, after the question.
        /// </summary>
        private void RemoveCurrent()
        {
            if (MessageBox.Show(localizator.RemoveProductText(), localizator.RemoveProductCaption(), MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Products.Remove(Product);
            }
        }

        /// <summary>
        /// A method that determines the availability of a command.
        /// </summary>
        private bool IsProductsZero()
        {
            return Products.Count == 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// A collection of products.
        /// </summary>
        public ObservableCollection<Product> Products { get; set; }

        /// <summary>
        /// A collection of all available categories.
        /// </summary>
        public ProductsCategories Categories { get; set; }

        /// <summary>
        /// A collection of all available manufacturers.
        /// </summary>
        public ProductsManufactureries Manufactureries { get; set; }

        /// <summary>
        /// Current product.
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
        /// Constructor of the main window (<see cref="DashboardWindow"/>) view
        /// model.
        /// </summary>
        public DashboardWindowViewModel()
        {
            // Set relative paths to the required files.
            fileOperation = new FileOperation(Products)
            {
                CollectionFileName = "Data/products.dat",
                SettingsFileName = "Data/settings.dat"
            };

            // Attempt to get from the file collection of products.
            Products = fileOperation.TryOpen();

            // If the attempt was unsuccessful, your need to create one
            // "standard" product to avoid unhandled exceptions.
            if (Products.Count == 0)
            {
                DefaultProduct(1);
            }

            // As the current product, the first is selected.
            Product = Products.FirstOrDefault();

            // Memory allocation for collections.
            Categories = new ProductsCategories();
            Manufactureries = new ProductsManufactureries();

            // Memory allocation on the object necessary for localization.
            localizator = new Localizator();

            // The window applies the necessary styles and localization.
            Apply();
        }

        /// <summary>
        /// Calls two methods
        /// (<see cref="ApplyTheme"/> и <see cref="Localize"/>), that set the
        /// necessary properties for the window.
        /// </summary>
        private void Apply()
        {
            ApplyTheme();
            Localize();
        }

        /// <summary>
        /// Applies a theme to the window.
        /// </summary>
        private void ApplyTheme()
        {
            fileOperation.Load();
            ThemeApplyer.Apply(fileOperation.Settings.Theme);
        }

        /// <summary>
        /// Localizes the window.
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
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Creates and adds a regular product. A common product is a product,
        /// which is a template to be filled by the user.
        /// </summary>
        /// <param name="id">Product id</param>
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
