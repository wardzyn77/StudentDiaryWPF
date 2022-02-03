using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentDiaryWPF.Models;
using StudentDiaryWPF.Commands;
using MahApps.Metro.Controls;
using System.Windows;
using StudentDiaryWPF.Models.Wrapes;

namespace StudentDiaryWPF.ViewModels
{
    class GroupAddEditViewModel : BaseViewModel
    {
        public GroupAddEditViewModel(GroupWraper group = null)
        {
            CancelCommand = new RelayCommand(Cancel);
            ConfirmCommand = new RelayCommand(Confirm);

            if (group == null)
                Group = new GroupWraper();
            else
            {
                Group = group;
            }
        }

        private GroupWraper _group;
        public GroupWraper Group
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChange();
            }
        }

        private void Confirm(object obj)
        {
            CloseWindows((Window)obj);
        }

        private void CloseWindows(Window window)
        {
            window.Close();
        }

        private void Cancel(object obj)
        {
            //obsługa bazy
            CloseWindows((Window)obj);
        }

        public ICommand CancelCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
    }
}
