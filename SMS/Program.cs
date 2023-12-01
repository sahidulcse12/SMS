using System;
using System.Security.Cryptography.X509Certificates;
using static SMS.Enums;

namespace SMS
{
    internal class Program
    {
        public static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            
            while (true)
            {

                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. Delete Student");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1: AddNewStudent(); break;
                    case 2: ViewStudent(students); break;
                    case 3: DeleteStudent(students); break;
                    default: Console.WriteLine("Invalid Choices"); break;
                }
            }
        }

        public static void AddNewStudent()
        {
            Console.WriteLine("Add a new student : ");
            string? firstName = Console.ReadLine();
            string? middleName = Console.ReadLine();
            string? lastName = Console.ReadLine();
            string? studentID = Console.ReadLine();
            int dept = Convert.ToInt32(Console.ReadLine());
            int degree = Convert.ToInt32(Console.ReadLine());

            Student newStudent = new Student
            {
                FirstName  = firstName,
                MidlleName  = middleName,
                LastName = lastName,
                StudentId = studentID,
                Department = (Dept)Enum.Parse(typeof(Dept), dept.ToString()),
                Degree = (Degree)Enum.Parse(typeof(Degree), dept.ToString()),
                JoinigBatch = new Semester { SemesterCode = "Summer", Year= DateTime.Now.Year.ToString()}
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
                Console.WriteLine($"Joining Batch: Semester Code : {student.JoinigBatch.SemesterCode}, Year : {student.JoinigBatch.Year}");
                Console.WriteLine($"Department: {student.Department}");
                Console.WriteLine($"Degree: {student.Degree}");
            }
            else
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
            }
        }

        static void DeleteStudent(List<Student> students)
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

    }
}