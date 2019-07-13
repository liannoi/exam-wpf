using Exam.FormUI.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exam.FormUI.Roles
{
    /// <summary>
    /// Роль для обеспечения работы с файловой системой.
    /// </summary>
    public class FileOperation
    {
        #region Fields

        /// <summary>
        /// Файловый поток.
        /// </summary>
        private FileStream fileStream;

        /// <summary>
        /// Объект ответственный за бинарное хранение и получение данных.
        /// </summary>
        private readonly BinaryFormatter binaryFormatter;

        /// <summary>
        /// Файловый диалог.
        /// </summary>
        private OpenFileDialog openFileDialog;

        /// <summary>
        /// Путь по которому необходимо скопировать файл, полученный в
        /// результате работы файлового диалога (<see cref="openFileDialog"/>).
        /// </summary>
        private string destination;

        #endregion

        #region Properties

        /// <summary>
        /// Путь к коллекции продуктов в сериализованном формате.
        /// </summary>
        public string CollectionFileName { get; set; }

        /// <summary>
        /// Путь к файлу с настройками в сериализованном формате.
        /// </summary>
        public string SettingsFileName { get; set; }

        /// <summary>
        /// Набор свойств из которого состоит лог файл (<see cref="SettingsFileName"/>).
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Коллекция продуктов.
        /// </summary>
        public ObservableCollection<Product> Products { get; set; }

        #endregion

        #region Methods

        public FileOperation(ObservableCollection<Product> products)
        {
            this.Products = products;
            binaryFormatter = new BinaryFormatter();
        }

        public FileOperation()
        {
            binaryFormatter = new BinaryFormatter();
        }

        /// <summary>
        /// Подготовка файлового диалога (<see cref="openFileDialog"/>) к открытию фотографии.
        /// </summary>
        public void InitializeDialog()
        {
            openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                Multiselect = false
            };
        }

        /// <summary>
        /// Сериализация коллекции с продуктами (<see cref="Products"/>).
        /// </summary>
        public void Serialize()
        {
            using (fileStream = new FileStream(CollectionFileName, FileMode.Create, FileAccess.Write))
            {
                binaryFormatter.Serialize(fileStream, Products);
            }
        }

        /// <summary>
        /// Десериализация коллекции с продуктами из файлового потока (<see cref="fileStream"/>).
        /// </summary>
        /// <returns>Коллекция продуктов</returns>
        public ObservableCollection<Product> Deserialize()
        {
            using (fileStream = new FileStream(CollectionFileName, FileMode.Open, FileAccess.Read))
            {
                Products = (ObservableCollection<Product>)binaryFormatter.Deserialize(fileStream);
                return Products;
            }
        }

        /// <summary>
        /// Попытка открыть файл с сериализированной коллекцией продуктов.
        /// Следует использовать если вы не уверены, создан ли бинарный файл по
        /// этому (<see cref="CollectionFileName"/>) пути.
        /// </summary>
        /// <returns>Коллекция продуктов</returns>
        public ObservableCollection<Product> TryOpen()
        {
            try
            {
                Products = Deserialize();
            }
            catch (DirectoryNotFoundException)
            {
                Products = new ObservableCollection<Product>();
            }
            catch (FileNotFoundException)
            {
                Products = new ObservableCollection<Product>();
            }
            return Products;
        }

        /// <summary>
        /// Метод оповещающий о статусе завершение файлового диалога
        /// (<see cref="openFileDialog"/>).
        /// </summary>
        public bool? IsCorrectDialog()
        {
            return openFileDialog.ShowDialog();
        }

        /// <summary>
        /// Создание папки.
        /// </summary>
        /// <param name="name">Название директории</param>
        public void CreateDirectory(string name)
        {
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(name);
            }
        }

        /// <summary>
        /// Копирование файла. Когда мы получаем файл от пользователя, нам
        /// необходимо поместить его в свою локальную директорию, для
        /// исключения исключительных ситуаций.
        /// </summary>
        public void CopyFile()
        {
            destination = $"Images/{Guid.NewGuid().ToString()}{Path.GetExtension(openFileDialog.FileName)}";
            File.Copy(openFileDialog.FileName, destination);
        }

        /// <summary>
        /// Возвращает новое фото для продукта.
        /// </summary>
        public Photo NewPhoto()
        {
            return new Photo
            {
                Path = Path.GetFullPath(destination)
            };
        }

        /// <summary>
        /// Сохранение настроек (<see cref="Settings"/>).
        /// </summary>
        public void Save()
        {
            try
            {
                using (fileStream = new FileStream(SettingsFileName, FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(fileStream, Settings);
                }
            }
            catch (DirectoryNotFoundException)
            {
                CreateDirectory("Data");
                Save();
            }
        }

        /// <summary>
        /// Загрузка настроек (<see cref="Settings"/>).
        /// </summary>
        public void Load()
        {
            using (fileStream = new FileStream(SettingsFileName, FileMode.Open, FileAccess.Read))
            {
                Settings = (Settings)binaryFormatter.Deserialize(fileStream);
            }
        }

        #endregion
    }
}
