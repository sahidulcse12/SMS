using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS.Enums;

namespace SMS
{
    public class StudentManagementMethods: ISmsHelpers
    {
        private static string StudentsFilePath = "students.json";

        public static List<Course> allCourses = new List<Course>{
        new Course { CourseId = "CSE 101", CourseName = "C Programming", InstructorName = "Prof. Nasir", NumberOfCredits = 3.5},
        new Course { CourseId = "CSE 102", CourseName = "Machine Learning", InstructorName = "Prof. Motiur", NumberOfCredits = 2.5},
        new Course { CourseId = "CSE 103", CourseName = "Java Programming", InstructorName = "Prof. Nasir", NumberOfCredits = 3.5},
        new Course { CourseId = "CSE 104", CourseName = "Arcitecture", InstructorName = "Prof. Motiur", NumberOfCredits = 2.5}
        };
        public static void AddNewStudent(List<Student> students)
        {
            Console.WriteLine("Add a new student : ");
            Console.WriteLine("First Name : ");
            string? firstName = Console.ReadLine();

            Console.WriteLine("Middle Name : ");
            string? middleName = Console.ReadLine();

            Console.WriteLine("Last Name : ");
            string? lastName = Console.ReadLine();

            Console.WriteLine("Student ID : ");
            string? studentID = Console.ReadLine();

            Console.WriteLine("Departnemt Number : ");
            int dept = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Degeree Number : ");
            int degree = Convert.ToInt32(Console.ReadLine());

            Student newStudent = new Student
            {
                FirstName = firstName,
                MidlleName = middleName,
                LastName = lastName,
                StudentId = studentID,
                Department = (Dept)Enum.Parse(typeof(Dept), dept.ToString()),
                Degree = (Degree)Enum.Parse(typeof(Degree), degree.ToString()),
                JoiningBatch = "Winter",
            };
            students.Add(newStudent);
        }

        public static void ViewStudent(List<Student> students)
        {
            Console.Write("Enter Student ID: ");
            string? studentId = Console.ReadLine();

            Student? student = students.Find(s => s.StudentId == studentId);

            if (student != null)
            {
                Console.WriteLine("Student Details:");
                Console.WriteLine($"Name: {student.FirstName} {student.MidlleName} {student.LastName}");
                Console.WriteLine($"Student ID: {student.StudentId}");
                Console.WriteLine($"Joining Batch: {student.JoiningBatch}");
                Console.WriteLine($"Department: {student.Department}");
                Console.WriteLine($"Degree: {student.Degree}");
                Console.WriteLine("Attended Courses Are : ");
                foreach (var semester in student.SemestersAttended)
                {
                    Console.WriteLine($"Semester: {semester.SemesterCode} {semester.Year}");
                    foreach (var course in student.AttendedCourse)
                    {
                        Console.WriteLine($"  Course ID: {course.CourseId}, Course Name: {course.CourseName}, Instructor: {course.InstructorName}, Credits: {course.NumberOfCredits}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
            }
        }

        public static void DeleteStudent(List<Student> students)
        {
            Console.Write("Enter Student ID to delete: ");
            string? studentIDToDelete = Console.ReadLine();

            Student? studentToDelete = students.Find(s => s.StudentId == studentIDToDelete);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);
                Console.WriteLine($"Student with ID {studentIDToDelete} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Student with ID {studentIDToDelete} not found.");
            }
        }

        public static void AddSemester(List<Student> students)
        {
            Console.Write("Enter Student ID: ");
            string? studentId = Console.ReadLine();
            Student? studentToFind = students.Find(s => s.StudentId == studentId);

            if (studentToFind != null)
            {
                Console.Write("Enter Semester Code (1: Summer, 2: Fall, 3: Spring): ");
                int sCode = Convert.ToInt32(Console.ReadLine());
                var semesterCode = (SemesterCode)Enum.Parse(typeof(SemesterCode), sCode.ToString());

                Console.Write("Enter Year (YYYY): ");
                string? year = Console.ReadLine();

                bool isInputetSemesterAttended = studentToFind.SemestersAttended.Any(semseter =>
                semseter.SemesterCode == semesterCode && semseter.Year == year);

                if (isInputetSemesterAttended)
                {
                    Console.WriteLine("Already attened the course");
                }
                else
                {
                    studentToFind.SemestersAttended.Add(new Semester { SemesterCode = semesterCode, Year = year });
                    Console.WriteLine("Available Courses");
                    var availableCourses = allCourses.Where(c => !studentToFind.AttendedCourse.Any(x => x.CourseId == c.CourseId)).ToList();
                    foreach (var course in availableCourses)
                    {
                        Console.WriteLine($"{course.CourseName}");
                    }

                    Console.WriteLine("How many courses do you want to take? : ");
                    int num = Convert.ToInt32(Console.ReadLine());

                    for (int i = 0; i < num; i++)
                    {
                        Console.Write("Enter Course ID to add (XXX YYY): ");
                        string? selectedCourseID = Console.ReadLine();

                        var course = availableCourses.Find(x => x.CourseId == selectedCourseID);
                        if (course != null)
                        {
                            studentToFind.AttendedCourse.Add(course);
                            course = null;
                        }
                        else
                        {
                            Console.WriteLine("course is not available");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        public static List<Student>? LoadStudents()
        {
            if (File.Exists(StudentsFilePath))
            {
                string json = File.ReadAllText(StudentsFilePath);
                return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
            }
            return new List<Student>();
        }
        public static void SaveStudents(List<Student> students)
        {
            string json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(StudentsFilePath, json);
        }
    }
}
