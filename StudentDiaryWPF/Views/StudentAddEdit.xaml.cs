using MahApps.Metro.Controls;
using StudentDiaryWPF.Models;
using StudentDiaryWPF.Models.Wrapes;

namespace StudentDiaryWPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy StudentAddEdit.xaml
    /// </summary>
    public partial class StudentAddEdit : MetroWindow
    {
        public StudentAddEdit(StudentWraper student = null)
        {
            InitializeComponent();
            //powiązanie widoku z viewModel - widok wie, gdzie ma szukać swoich danych
            DataContext = new ViewModels.StudentAddEditViewModel(student);
        }
    }
}
