﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SaintSender.Core.Entities;

namespace SaintSender.DesktopUI.ViewModels
{
    public class SingleMailViewModel : INotifyPropertyChanged
    {
        private ICommand _saveButton;
        private Mail _mail;
        private Action _closeaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttributeViewModel"/> class.
        /// </summary>
        /// <param name="file">FileInfo</param>
        public SingleMailViewModel(Mail mail)
        {
            Mail = new Mail(mail.Sender, mail.Subject,mail.Message, mail.Date,mail.Read);
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets File.
        /// </summary>
        public Mail Mail
        {
            get
            {
                return _mail;
            }

            set
            {
                _mail = value;
            }
        }

        /// <summary>
        /// Gets or Sets CloseAction.
        /// </summary>
        public Action CloseAction
        {
            get
            {
                return _closeaction;
            }

            set
            {
                _closeaction = value;
            }
        }



        private void MyFilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyRaised("FilteredFile");
        }

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        /// <summary>
        /// Inner helper class to store different File properties and attributes.
        /// </summary>
        public class MyFile : INotifyPropertyChanged
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MyFile"/> class.
            /// </summary>
            /// <param name="file">Used to initialize properties.</param>
            public MyFile(FileInfo file)
            {
                Name = Path.GetFileNameWithoutExtension(file.FullName);
                Extension = file.Extension;
                Hidden = file.Attributes.HasFlag(FileAttributes.Hidden);
            }

            /// <inheritdoc/>
            // parent Class subscribes the PropertyChanged event ("warning: not used" is due to that)
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Gets or sets the Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or Sets Extension.
            /// </summary>
            public object Extension { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether is hidden or not.
            /// </summary>
            public bool Hidden { get; set; }

            // private void RaiseChanged(string propName)
            // {
            //    if (PropertyChanged != null)
            //    {
            //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
            //    }
            // }
        }

        /// <summary>
        /// Handles Save button event.
        /// </summary>
        protected class SaveCommand : ICommand
        {
            private FileInfo _originalFile;
            private MyFile _myfile;
            private Action _closeaction;

            /// <summary>
            /// Initializes a new instance of the <see cref="SaveCommand"/> class.
            /// </summary>
            /// <param name="file">Reference of the file storing class.</param>
            /// <param name="originalfile">FileInfo reference.</param>
            /// <param name="closeaction">Delegate a method.</param>
            public SaveCommand(ref MyFile file, ref FileInfo originalfile, ref Action closeaction)
            {
                _myfile = file;
                _originalFile = originalfile;
                _closeaction = closeaction;
            }

            /// <inheritdoc/>
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            /// <inheritdoc/>
            public bool CanExecute(object parameter)
            {
                if ((string)parameter == string.Empty)
                {
                    return false;
                }

                return true;
            }

            /// <inheritdoc/>
            public void Execute(object parameter)
            {
                string updatedPath = RenameFile();
                SetOrRemoveHiddenAttribute(updatedPath);
                if (updatedPath != null)
                {
                    _closeaction();
                }
            }

            private string RenameFile()
            {
                string newFilename = _myfile.Name + _myfile.Extension;
                if (newFilename == _originalFile.Name)
                {
                    return _originalFile.FullName;
                }

                // file could be deleted during the process, hence the check
                // if (new FileInfo(_originalFile.FullName).Exists)
                try
                {
                    string newPath = Path.GetDirectoryName(_originalFile.FullName) + "\\" + newFilename;
                    _originalFile.MoveTo(newPath);
                    SetOrRemoveHiddenAttribute(newPath);
                    MessageBox.Show("File renamed.");
                    return newPath;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return null;
                }
            }

            private void SetOrRemoveHiddenAttribute(string path)
            {
                // if the hidden flag is not changed, return.
                if (_myfile.Hidden == _originalFile.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    return;
                }

                try
                {
                    // based on MSDN "File.SetAttributes(String, FileAttributes) Method"
                    FileAttributes attributes = System.IO.File.GetAttributes(path);

                    // check if the newly set value is Hidden (true) or not (false).
                    if (_myfile.Hidden)
                    {
                        // hide the file
                        // explanation: https://stackoverflow.com/a/28003937
                        System.IO.File.SetAttributes(path, attributes | FileAttributes.Hidden);
                    }
                    else
                    {
                        // show the file
                        attributes = attributes & ~FileAttributes.Hidden;
                        System.IO.File.SetAttributes(path, attributes);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
