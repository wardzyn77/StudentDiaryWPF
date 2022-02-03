﻿
namespace StudentDiaryWPF.Models.Wrapes
{
    public class StudentWraper
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
    }
}
