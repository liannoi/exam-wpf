﻿using Exam.FormUI.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exam.FormUI.Roles
{
    /// <summary>
    /// The role to ensure the work with the file system.
    /// </summary>
    public class FileOperation
    {
        #region Fields

        private FileStream fileStream;

        /// <summary>
        /// The object is responsible for the binary storage and retrieval of
        /// data.
        /// </summary>
        private readonly BinaryFormatter binaryFormatter;

        private OpenFileDialog openFileDialog;

        /// <summary>
        /// Path where it is necessary to copy the file obtained as a result of
        /// the file dialog (<see cref="openFileDialog"/>).
        /// </summary>
        private string destination;

        #endregion

        #region Properties

        /// <summary>
        /// Path to the collection of products in a serialized format.
        /// </summary>
        public string CollectionFileName { get; set; }

        /// <summary>
        /// The path to the file with the settings in the serialized format.
        /// </summary>
        public string SettingsFileName { get; set; }

        /// <summary>
        /// The property set of which the log file
        /// (<see cref="SettingsFileName"/>) consists.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// A collection of products.
        /// </summary>
        public ObservableCollection<Product> Products { get; set; }

        #endregion

        #region Methods

        public FileOperation(ObservableCollection<Product> products)
        {
            Products = products;
            binaryFormatter = new BinaryFormatter();
        }

        public FileOperation()
        {
            binaryFormatter = new BinaryFormatter();
        }

        /// <summary>
        /// Preparing a file dialog (<see cref="openFileDialog"/>) for opening
        /// a photo.
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
        /// Serialization of the collection with products
        /// (<see cref="Products"/>).
        /// </summary>
        public void Serialize()
        {
            using (fileStream = new FileStream(CollectionFileName, FileMode.Create, FileAccess.Write))
            {
                binaryFormatter.Serialize(fileStream, Products);
            }
        }

        /// <summary>
        /// Deserialization of the collection with products from the file
        /// stream (<see cref="fileStream"/>).
        /// </summary>
        /// <returns>Product collection</returns>
        public ObservableCollection<Product> Deserialize()
        {
            using (fileStream = new FileStream(CollectionFileName, FileMode.Open, FileAccess.Read))
            {
                Products = (ObservableCollection<Product>)binaryFormatter.Deserialize(fileStream);
                return Products;
            }
        }

        /// <summary>
        /// Attempting to open a file with a serialized collection of products.
        /// It should be used if your are not sure whether a binary file is
        /// created on this (<see cref="CollectionFileName"/>) path.
        /// </summary>
        /// <returns>Product collection</returns>
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
        /// The method of notifying about the status of completion of the file
        /// dialog (<see cref="openFileDialog"/>).
        /// </summary>
        public bool? IsCorrectDialog()
        {
            return openFileDialog.ShowDialog();
        }

        /// <summary>
        /// Creating a folder.
        /// </summary>
        /// <param name="name">Directory name</param>
        public void CreateDirectory(string name)
        {
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(name);
            }
        }

        /// <summary>
        /// When we receive a file from the user, we need to place it in our
        /// local directory, to exclude exceptions.
        /// </summary>
        public void CopyFile()
        {
            destination = $"Images/{Guid.NewGuid().ToString()}{Path.GetExtension(openFileDialog.FileName)}";
            File.Copy(openFileDialog.FileName, destination);
        }

        /// <summary>
        /// Returns a new photo for the product.
        /// </summary>
        public Photo NewPhoto()
        {
            return new Photo
            {
                Path = Path.GetFullPath(destination)
            };
        }

        /// <summary>
        /// Saving settings (<see cref="Settings"/>).
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
        /// Loading settings (<see cref="Settings"/>).
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
