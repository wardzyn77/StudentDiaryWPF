using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Models;
using StudentDiaryWPF.Views;

namespace StudentDiaryWPF.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            RefreshStudentCommand = new RelayCommand(RefreshStudent);
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, IsSelectedStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, IsSelectedStudent);
            RefreshDiary();
        }

        public ICommand RefreshStudentCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }

        private Student _selectedStudent;
        private ObservableCollection<Student> _students;     //lepiej niż zamiast >> List<Student>
        private ObservableCollection<Group> _cmbSearchGroupSource;
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

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChange();
            }
        }

        public ObservableCollection<Group> CmbSearchGroupSource
        {
            get { return _cmbSearchGroupSource; }
            set
            {
                _cmbSearchGroupSource = value;
                OnPropertyChange();
            }
        }

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChange();
            }
        }

        private void RefreshStudent(object obj)
        {
            List<Student> list = new List<Student>();
            //list = (List < Student > )_students;
            RefreshDiary();
        }

        private void RefreshDiary()
        {
                //29 minuta
            //odśwież z pliku lub z bazy
            _students = new ObservableCollection<Student>
            {
                new Student {FirstName = "Jan", LastName = "Nowak", Group = new Group {Id=1} },
                new Student {FirstName = "Jancio", LastName = "Muzykant", Group = new Group {Id=2} },
                new Student {FirstName = "Piotr", LastName = "Wardzyn", Group = new Group {Id=2} }
            };
            _cmbSearchGroupSource = new ObservableCollection<Group>
            {
                new Group {Id=1, Name = "Wszyscy"},
                new Group {Id=1, Name = "1A"},
                new Group {Id=2, Name = "2A"},
                new Group {Id=3, Name = "3A"}
            };
        }

        private bool IsSelectedStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private void AddEditStudent(object obj)
        {
            //docelowo jest to nie do końca dobre rozwiązanie - dla testów jednostkowych ALE OK
            var addEditStudentWindow = new StudentAddEdit(obj as Student);
            addEditStudentWindow.Closed += addEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void addEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);
            
            if (dialog != MessageDialogResult.Affirmative)
                return;
            //usuwanie ucznia z bazy

            RefreshDiary();
        }

    }
}
