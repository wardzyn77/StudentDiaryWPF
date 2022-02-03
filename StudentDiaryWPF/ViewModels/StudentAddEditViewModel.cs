using StudentDiaryWPF.Models;
using System.Windows.Input;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using StudentDiaryWPF.Models.Wrapes;
using StudentDiaryWPF.Models.Domains;

namespace StudentDiaryWPF.ViewModels
{
    class StudentAddEditViewModel : BaseViewModel
    {
        public StudentAddEditViewModel(StudentWraper student = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Close);

            if (student != null)
            {
                IsUpdate = true;
                Student = student;
            }
            else
                Student = new StudentWraper();
            InitGroups();
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private StudentWraper _student;

        public StudentWraper Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChange();
            }
        }

        private Repository _repository = new Repository();
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
            _repository.UpdateStudent(Student);
        }

        private void AddStudent()
        {
            _repository.AddStudent(Student);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-- brak --" });

            _groups = new ObservableCollection<Group>(groups);
            
            Student.Group.Id = 0;
        }
    }
}
