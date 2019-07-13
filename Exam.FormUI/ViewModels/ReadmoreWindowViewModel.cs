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
    /// Модель представления для окна <see cref="ReadmoreWindow"/>.
    /// </summary>
    public class ReadmoreWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Объект класса ответственного за операции с файлами.
        /// </summary>
        private readonly FileOperation fileOperation;

        /// <summary>
        /// Текущая фотография.
        /// </summary>
        private Photo photo;

        /// <summary>
        /// Индекс текущего продукта в коллекции.
        /// </summary>
        private int index;

        /// <summary>
        /// Коллекция всех продуктов.
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
        /// Добавляет фотографию к коллекции (<see cref="Photos"/>) фотографий
        /// продукта.
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

            // Сохранение фото.
            products[index].Photos = Photos;
            fileOperation.Products = products;
            fileOperation.Serialize();
        }

        /// <summary>
        /// Удаляет текущею фотографию с коллекции (<see cref="Photos"/>)
        /// фотографий у продукта.
        /// </summary>
        private void Remove()
        {
            Photos.Remove(Photo);
            Photo = Photos.FirstOrDefault();
        }

        /// <summary>
        /// Метод определяющий доступ к команде.
        /// </summary>
        /// <returns>
        /// Является ли текущая фотография первой из всей коллекции (<see cref="Photos"/>) фотографий у продукта.
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
        /// Метод определяющий доступ к команде.
        /// </summary>
        /// <returns>
        /// Является ли текущая фотография последней из всей коллекции (<see cref="Photos"/>) фотографий у продукта.
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
        /// Метод определяющий доступ к команде.
        /// </summary>
        private bool IsLastPosition()
        {
            return Photos.IndexOf(Photo) == Photos.Count - 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Коллекция всех фотографий текущего продукта.
        /// </summary>
        public ProductsPhotos Photos { get; set; }

        /// <summary>
        /// Текущая фотография.
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
        /// Код товара. В него записываться значение <see cref="Product.Id"/>.
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Конструктор модели представления окна <see cref="ReadmoreWindow"/>.
        /// </summary>
        /// <param name="photos">Коллекция фотографий текущего продукта.</param>
        public ReadmoreWindowViewModel(ObservableCollection<Product> products, int index, ProductsPhotos photos)
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

        public void OnPropertyChanged([CallerMemberName]string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
