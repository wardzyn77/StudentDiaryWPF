
using System.ComponentModel;
using StudentDiaryWPF.Models.Configurations;

namespace StudentDiaryWPF.Models.Wrapes
{
    public class StudentWraper : IDataErrorInfo
    {
        public StudentWraper()
        {
            Group = new GroupWraper();
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
        public bool Activities { get; set; }
        public string Comments { get; set; }
        public GroupWraper Group { get; set; }

        public string Error { get; set; }
        //private int _firstNameLen = StudentConfiguration
        public bool IsValid 
        {
            get
            {
                return IsValidFirstName && IsValidLastName;
            }
        }
        private bool IsValidFirstName = false, IsValidLastName = false;

        public string this[string columnName]
        {
            get
            {
                Error = string.Empty;
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                            Error = "Pole imię jest wymagane";
                        else
                            IsValidFirstName = true;
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                            Error = "Pole nazwisko jest wymagane";
                        else
                            IsValidLastName = true;
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        
    }
}
