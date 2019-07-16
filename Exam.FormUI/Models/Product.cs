using Exam.FormUI.Models.ObservableCollections;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exam.FormUI.Models
{
    /// <summary>
    /// Product model.
    /// </summary>
    [Serializable]
    public class Product : INotifyPropertyChanged
    {
        #region Fields

        private int id;
        private string name;
        private Category category;
        private Manufacturer manufacturer;
        private int year;
        private decimal price;
        private ProductsPhotos photos;

        #endregion

        #region NotifyPropertyChanged Properties

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public Category Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
                OnPropertyChanged();
            }
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return manufacturer;
            }

            set
            {
                manufacturer = value;
                OnPropertyChanged();
            }
        }

        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        public ProductsPhotos Photos
        {
            get
            {
                return photos;
            }

            set
            {
                photos = value;
                OnPropertyChanged();
            }
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

        /// <summary>
        /// Not serialized with binary serialization.
        /// </summary>
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
