using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Views;
using StudentDiaryWPF.Models.Wrapes;
using System.Linq;
using StudentDiaryWPF.Models.Domains;

namespace StudentDiaryWPF.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            //using (var context = new AplicationDBContext())
            //{
            //    var students = context.Students.ToList();
            //}
            RefreshStudentCommand = new RelayCommand(RefreshStudent);
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, IsSelectedStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, IsSelectedStudent);
            EditGroupCommand = new RelayCommand(EditGroup);

            InitGroups();
            InitStudents();
            RefreshDiary();
        }

        public ICommand RefreshStudentCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand EditGroupCommand { get; set; }

        private Student _selectedStudent;
        private ObservableCollection<StudentWraper> _students;     //lepiej niż zamiast >> List<Student> 
        private ObservableCollection<Group> _cmbSearchGroupSource;
        private int _selectedGroupId;
        private List<Group> _groups;
        private Repository _repository = new Repository();
        
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

        public ObservableCollection<StudentWraper> Students
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
            List<StudentWraper> list = new List<StudentWraper>();
            //list = (List < Student > )_students;
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            //29 minuta
            //odśwież z pliku lub z bazy
            var students = _repository.GetStudents(SelectedGroupId);

            Students = new ObservableCollection<StudentWraper>(students);

        }

        private void InitStudents()
        {
            var students = _repository.GetStudents(0);

            _students = new ObservableCollection<StudentWraper>(students);

        }

        private void InitGroups()
        {
            _groups = _repository.GetGroups();
            _groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            _cmbSearchGroupSource = new ObservableCollection<Group>(_groups);

            SelectedGroupId = 0;
        }

        private bool IsSelectedStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private void AddEditStudent(object obj)
        {
            //docelowo jest to nie do końca dobre rozwiązanie - dla testów jednostkowych ALE OK
            var addEditStudentWindow = new StudentAddEdit(obj as StudentWraper);
            //var s = new StudentAddEditViewModel(obj as Student);
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
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?"
                , MessageDialogStyle.AffirmativeAndNegative);
            
            if (dialog != MessageDialogResult.Affirmative)
                return;
            //usuwanie ucznia z bazy
            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        private void EditGroup(object obj)
        {
            var addEditwindow = new GroupAddEdit(obj as GroupWraper);
            addEditwindow.Closed += AddEditwindow_Closed;
            addEditwindow.ShowDialog();
        }

        private void AddEditwindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
