using StudentDiaryWPF.Models;
using System.Windows.Input;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentDiaryWPF.ViewModels
{
    class StudentAddEditViewModel : BaseViewModel
    {
        public StudentAddEditViewModel(Student student = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Close);

            if (student != null)
            {
                IsUpdate = true;
                Student = student;
            }
            else
                Student = new Student();
            InitGroups();
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChange();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChange();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChange();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChange();
            }
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void Confirm(object obj)
        {   //w xaml widoku przy button przekazujemy poza comendą jej parametr - nazwę okna
            if (IsUpdate)
                UpdateStudent();
            else
                AddStudent();
            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            //baza danych
        }

        private void AddStudent()
        {
            //baza danych
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitGroups()
        {
            _groups = new ObservableCollection<Group>
            {
                new Group {Id=0, Name = "--Brak--"},
                new Group {Id=1, Name = "1A"},
                new Group {Id=2, Name = "2A"},
                new Group {Id=3, Name = "3A"}
            };
            //Student.Group.Id = 0;
        }
    }
}
