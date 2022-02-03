using StudentDiaryWPF.Models.Domains;
using StudentDiaryWPF.Models.Wrapes;
using StudentDiaryWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDiaryWPF.Models.Converters
{
    public static class StudentConverter
    {
        public static StudentWraper ToWraper (this Student model)
        {
            return new StudentWraper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                Group = new GroupWraper { Id = model.Group.Id, Name = model.Group.Name },
                Math = string.Join(", ", model.Ratings
                    .Where(x => x.SubjectId == (int)Subject.Math)
                    .Select(x => x.Rate)),
                Technology = string.Join(", ", model.Ratings
                    .Where(x => x.SubjectId == (int)Subject.Technology)
                    .Select(x => x.Rate)),
                PolishLang = string.Join(", ", model.Ratings
                    .Where(x => x.SubjectId == (int)Subject.PolishLang)
                    .Select(x => x.Rate)),
                ForeignLang = string.Join(", ", model.Ratings
                    .Where(x => x.SubjectId == (int)Subject.ForeignLang)
                    .Select(x => x.Rate)),
                Physics = string.Join(", ", model.Ratings
                    .Where(x => x.SubjectId == (int)Subject.Physics)
                    .Select(x => x.Rate)),
            };
        }

        public static Student ToDao(this StudentWraper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                GroupId = model.Group.Id,
                Comments = model.Comments,
                Activities = model.Activities
            };
        }

        public static List<Rating> ToRatingDao(this StudentWraper model)
        {
            var ratings = new List<Rating>();
            if (!string.IsNullOrWhiteSpace(model.Math))
                model.Math.Split(',').ToList()
                    .ForEach(x => ratings.Add(
                        new Rating
                        {
                            Id = model.Id,
                            Rate =int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Math
                        }
                        ));
            if (!string.IsNullOrWhiteSpace(model.Technology))
                model.Technology.Split(',').ToList()
                    .ForEach(x => ratings.Add(
                        new Rating
                        {
                            Id = model.Id,
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Technology
                        }
                        ));
            if (!string.IsNullOrWhiteSpace(model.Physics))
                model.Physics.Split(',').ToList()
                    .ForEach(x => ratings.Add(
                        new Rating
                        {
                            Id = model.Id,
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Physics
                        }
                        ));
            if (!string.IsNullOrWhiteSpace(model.PolishLang))
                model.PolishLang.Split(',').ToList()
                    .ForEach(x => ratings.Add(
                        new Rating
                        {
                            Id = model.Id,
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.PolishLang
                        }
                        ));
            if (!string.IsNullOrWhiteSpace(model.ForeignLang))
                model.ForeignLang.Split(',').ToList()
                    .ForEach(x => ratings.Add(
                        new Rating
                        {
                            Id = model.Id,
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.ForeignLang
                        }
                        ));
            return ratings;
        }
    }
}
