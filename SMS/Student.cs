using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS.Enums;

namespace SMS
{
    public class Student
    {
        public Student()
        {
            Semesters = new List<Semester>();
            Courses = new List<Course>();
            JoinigBatch = new Semester();
        }

        public string FirstName {get;set;}

        public string LastName { get; set; } = string.Empty;
        public string MidlleName {  get;set;} = string.Empty;
        public string StudentId { get;set;} = string.Empty;
        public Semester JoinigBatch { get;set;}
        public Dept Department { get; set; } = Dept.None;
        public Degree  Degree { get; set; } = Degree.None;
        public List<Course> Courses { get; set; } 

        public List<Semester> Semesters { get; set; }
    }
}
