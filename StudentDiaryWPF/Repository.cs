using StudentDiaryWPF.Models.Domains;
using StudentDiaryWPF.Models.Wrapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using StudentDiaryWPF.Models.Converters;
using System.Text;
using System.Threading.Tasks;
using StudentDiaryWPF.Models;

namespace StudentDiaryWPF
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new AplicationDBContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWraper> GetStudents(int groupId)
        {
            using (var context = new AplicationDBContext())
            {
                var students1 = context.Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .Where(x => x.GroupId == groupId || groupId == 0);
                    //.ToList();  BRAK powoduje, że jeszcze NIE jest wywołane
                //przy poniższym zastosowaliśmy ROZSZERZENIE o nową metodę ToWraper - można to robić dla każdej klasy
                return students1.ToList().Select(x => x.ToWraper()).ToList();
                var students2 = context.Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .AsQueryable();
                if (groupId != 0)
                    students2 = students2.Where(x => x.GroupId == groupId);
                return students2.ToList().Select(x => x.ToWraper()).ToList();
            }
        }

        internal void DeleteStudent(int selectedStudentId)
        {
            using (var context = new AplicationDBContext())
            {
                var studentToDelete = context.Students.Find(selectedStudentId);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            };
        }

        public void UpdateStudent(StudentWraper studentWraper)
        {
            var studentNew = studentWraper.ToDao();
            var ratingsNew = studentWraper.ToRatingDao();

            using (var context = new AplicationDBContext())
            {
                var dbStudentToUpd = context.Students.Find(studentNew.Id);
                dbStudentToUpd.Activities = studentNew.Activities;
                dbStudentToUpd.FirstName = studentNew.FirstName;
                dbStudentToUpd.LastName = studentNew.LastName;
                dbStudentToUpd.Comments = studentNew.Comments;
                dbStudentToUpd.GroupId = studentNew.GroupId;

                var studentsRatingOld = context.Ratings
                    .Where(x => x.StudentId == studentNew.Id).ToList();
                var subjects = new Subject();
                // ok 50 min
                //foreach (var subject in subjects)
                
                {
                    UpdateRate(studentNew, ratingsNew, context, studentsRatingOld, Subject.Math);
                }
            }
        }

        private void UpdateRate(Student student, List<Rating> ratingsNew, AplicationDBContext context, List<Rating> studentsRatingsOld, Subject subject)
        {
            var subjectRatings = studentsRatingsOld.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);
            var subjectRatingsNew = ratingsNew.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);
            var ratingToDelete = subjectRatings.Except(subjectRatingsNew).ToList();
            var ratingToAdd = subjectRatingsNew.Except(subjectRatings).ToList();

            ratingToDelete.ForEach(x =>
            {
                var ratingToDel = context.Ratings.First(y => 
                y.Rate == x &&
                y.StudentId == student.Id &&
                y.SubjectId == (int)subject);
                context.Ratings.Remove(ratingToDel);
            });
        }

        public void AddStudent(StudentWraper studentWraper)
        {
            var studentToAdd = studentWraper.ToDao();
            var ratings = studentWraper.ToRatingDao();

            using (var context = new AplicationDBContext())
            {
                var dbStudent = context.Students.Add(studentToAdd);
                ratings.ForEach(x =>
                { 
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });
                context.SaveChanges();
            }
        }
    }
}
