using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using static SMS.Enums;

namespace SMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var students =StudentManagementMethods.LoadStudents();
            while (true)
            {
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Add Semester");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1: StudentManagementMethods.AddNewStudent(students); break;
                    case 2: StudentManagementMethods.ViewStudent(students); break;
                    case 3: StudentManagementMethods.DeleteStudent(students); break;
                    case 4: StudentManagementMethods.AddSemester(students); break;
                    default: Console.WriteLine("Invalid Choices"); break;
                }
                StudentManagementMethods.SaveStudents(students);
            }
        }
    }
}