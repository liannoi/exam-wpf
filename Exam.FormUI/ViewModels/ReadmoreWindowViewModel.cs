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

using Exam.FormUI.Models;
using Exam.FormUI.Models.ObservableCollections;
using Exam.FormUI.Roles;
using Exam.FormUI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Exam.FormUI.ViewModels
{
    /// <summary>
    /// The view model for the window <see cref="ReadmoreWindow"/>.
    /// </summary>
    public class ReadmoreWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The class object responsible for file operations.
        /// </summary>
        private readonly FileOperation fileOperation;

        /// <summary>
        /// Current photo.
        /// </summary>
        private Photo photo;

        /// <summary>
        /// The index of the current product in the collection.
        /// </summary>
        private int index;

        /// <summary>
        /// A collection of all products.
        /// </summary>
        private ObservableCollection<Product> products;

        #endregion

        #region Commands

        private RelayCommand backCommand;
        private RelayCommand forwardCommand;
        private RelayCommand removeCommand;
        private RelayCommand addCommand;

        public ICommand BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new RelayCommand(action => Photo = Photos[Photos.IndexOf(photo) - 1], o => !IsFirst());
                }
                return backCommand;
            }
        }

        public ICommand ForwardCommand
        {
            get
            {
                if (forwardCommand == null)
                {
                    forwardCommand = new RelayCommand(action => Photo = Photos[Photos.IndexOf(photo) + 1], o => !IsLastPosition());
                }
                return forwardCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new RelayCommand(action => Remove(), o => !IsLast());
                }
                return removeCommand;
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

        /// <summary>
        /// Adds a photo to the collection (<see cref="Photos"/>) of product
        /// photos.
        /// </summary>
        private void Add()
        {
            if (fileOperation.IsCorrectDialog() == false)
            {
                return;
            }
            fileOperation.CreateDirectory("Images");
            fileOperation.CopyFile();
            Photos.Add(fileOperation.NewPhoto());
            Photo = Photos.FirstOrDefault();

            // Save the photo.
            products[index].Photos = Photos;
            fileOperation.Products = products;
            fileOperation.Serialize();
        }

        /// <summary>
        /// Removes the current photo from the photo collection
        /// (<see cref="Photos"/>) from the product.
        /// </summary>
        private void Remove()
        {
            Photos.Remove(Photo);
            Photo = Photos.FirstOrDefault();
        }

        /// <summary>
        /// Method defines access to the command.
        /// </summary>
        /// <returns>
        /// Whether the current photo is the first from the entire collection (<see cref="Photos"/>) of the photos of the product.
        /// </returns>
        private bool IsFirst()
        {
            if (Photos.Count == 0)
            {
                return true;
            }
            return Photos.IndexOf(Photo) == 0;
        }

        /// <summary>
        /// Method defines access to the command.
        /// </summary>
        /// <returns>
        /// Is the current photo the latest from the entire collection (<see cref="Photos"/>) of the photos of the product.
        /// </returns>
        private bool IsLast()
        {
            if (Photos.Count == 0)
            {
                return true;
            }
            return Photos.Count - 1 == -1;
        }

        /// <summary>
        /// Method defines access to the command.
        /// </summary>
        private bool IsLastPosition()
        {
            return Photos.IndexOf(Photo) == Photos.Count - 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// A collection of all photos of the current product.
        /// </summary>
        public ProductsPhotos Photos { get; set; }

        /// <summary>
        /// Current photo.
        /// </summary>
        public Photo Photo
        {
            get => photo;

            set
            {
                photo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product code. Write the value <see cref="Product.Id"/>.
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Constructor of the <see cref="ReadmoreWindow"/> window view model.
        /// </summary>
        /// <param name="photos">A collection of photos of the current product.</param>
        public ReadmoreWindowViewModel(ObservableCollection<Product> products,
            int index,
            ProductsPhotos photos)
        {
            Photos = photos;
            if (Photos == null)
            {
                Photos = new ProductsPhotos();
            }
            Photo = Photos.FirstOrDefault();
            fileOperation = new FileOperation
            {
                CollectionFileName = "Data/products.dat"
            };
            fileOperation.InitializeDialog();
            this.products = products;
            this.index = index;
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
