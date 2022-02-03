﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudentDiaryWPF.Models.Domains
{
    public class Student
    {
        public Student()
        {
            Ratings = new Collection<Rating>();
            //Group = new Group();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Activities { get; set; }
        public string Comments { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
