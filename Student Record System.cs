using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Record_System
{
    internal class Student_Record_System
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                StudentManager studentManager = new StudentManager();
                studentManager.Run();
            }
        }

        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int YearLevel { get; set; }
            public string Course { get; set; } // Full course name
            public double Grade { get; set; }

            public Student(int id, string name, int yearLevel, string course, double grade)
            {
                ID = id;
                Name = name;
                YearLevel = yearLevel;
                Course = course;
                Grade = grade;
            }
        }

        public class StudentManager
        {
            private Student[] students;
            private int count;

            private Dictionary<string, string> validCourses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "BSIT", "Bachelor of Science in Information Technology" },
            { "BSCS", "Bachelor of Science in Computer Science" },
            { "BSED", "Bachelor of Secondary Education" },
            { "BEED", "Bachelor of Elementary Education" },
            { "BSBA", "Bachelor of Science in Business Administration" },
            { "BSCRIM", "Bachelor of Science in Criminology" },
            { "BSAIS", "Bachelor of Science in Accounting Information System" },
            { "BSHM", "Bachelor of Science in Hospitality Management" },
            { "BSTM", "Bachelor of Science in Tourism Management" },
            { "BSN", "Bachelor of Science in Nursing" },
            { "BSCE", "Bachelor of Science in Civil Engineering" },
            { "BSEE", "Bachelor of Science in Electrical Engineering" },
            { "BSECE", "Bachelor of Science in Electronics Engineering" },
            { "BSME", "Bachelor of Science in Mechanical Engineering" },
            { "BSA", "Bachelor of Science in Accountancy" },
            { "BSMA", "Bachelor of Science in Management Accounting" },
            { "BSPsych", "Bachelor of Science in Psychology" },
            { "BSBio", "Bachelor of Science in Biology" }
        };

            public StudentManager()
            {
                students = new Student[100];
                count = 0;
            }

            public void Run()
            {
                int choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Student Record System");
                    Console.WriteLine("1. Add Student");
                    Console.WriteLine("2. View Students");
                    Console.WriteLine("3. Update Student");
                    Console.WriteLine("4. Delete Student");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choose an option: ");
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }

                    switch (choice)
                    {
                        case 1: AddStudent(); break;
                        case 2: ViewStudents(); break;
                        case 3: UpdateStudent(); break;
                        case 4: DeleteStudent(); break;
                        case 5: Console.WriteLine("Exiting..."); break;
                        default: Console.WriteLine("Invalid choice. Please try again."); break;
                    }

                    if (choice != 5)
                    {
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }

                } while (choice != 5);
            }

            private void AddStudent()
            {
                int id;
                while (true)
                {
                    Console.Write("Enter Student ID (numbers only): ");
                    if (int.TryParse(Console.ReadLine(), out id)) break;
                    Console.WriteLine("Invalid ID. Please enter numeric values only.");
                }

                string name;
                while (true)
                {
                    Console.Write("Enter Student Name (letters only): ");
                    name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name) && IsAllLetters(name)) break;
                    Console.WriteLine("Invalid name. Please enter letters only.");
                }

                Console.Write("Enter Year Level: ");
                if (!int.TryParse(Console.ReadLine(), out int yearLevel))
                {
                    Console.WriteLine("Invalid Year Level input. Must be a number.");
                    return;
                }

                string course;
                while (true)
                {
                    Console.Write("Enter Course : ");
                    string input = Console.ReadLine().Trim();
                    course = GetFullCourseName(input);
                    if (course != null) break;
                    Console.WriteLine("Invalid course. Please enter a valid course abbreviation or full name.");
                }

                Console.Write("Enter Grade: ");
                if (!double.TryParse(Console.ReadLine(), out double grade))
                {
                    Console.WriteLine("Invalid Grade input. Must be a number.");
                    return;
                }

                students[count] = new Student(id, name, yearLevel, course, grade);
                count++;
                Console.WriteLine("Student added successfully.");
            }

            private void ViewStudents()
            {
                if (count == 0)
                {
                    Console.WriteLine("No students to display.");
                    return;
                }

                Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-45} | {4,-5}", "ID", "Name", "Year", "Course", "Grade");
                Console.WriteLine(new string('-', 95));
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-45} | {4,-5}",
                        students[i].ID,
                        students[i].Name,
                        students[i].YearLevel,
                        students[i].Course,
                        students[i].Grade);
                }
            }

            private void UpdateStudent()
            {
                int id;
                while (true)
                {
                    Console.Write("Enter Student ID to update (numbers only): ");
                    if (int.TryParse(Console.ReadLine(), out id)) break;
                    Console.WriteLine("Invalid ID. Please enter numeric values only.");
                }

                for (int i = 0; i < count; i++)
                {
                    if (students[i].ID == id)
                    {
                        string name;
                        while (true)
                        {
                            Console.Write("Enter new Name (letters only): ");
                            name = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(name) && IsAllLetters(name)) break;
                            Console.WriteLine("Invalid name. Please enter letters only.");
                        }
                        students[i].Name = name;

                        Console.Write("Enter new Year Level: ");
                        if (!int.TryParse(Console.ReadLine(), out int yearLevel))
                        {
                            Console.WriteLine("Invalid Year Level input. Must be a number.");
                            return;
                        }
                        students[i].YearLevel = yearLevel;

                        string course;
                        while (true)
                        {
                            Console.Write("Enter new Course (abbreviation or full name): ");
                            string input = Console.ReadLine().Trim();
                            course = GetFullCourseName(input);
                            if (course != null) break;
                            Console.WriteLine("Invalid course. Please enter a valid course abbreviation or full name.");
                        }
                        students[i].Course = course;

                        Console.Write("Enter new Grade: ");
                        if (!double.TryParse(Console.ReadLine(), out double grade))
                        {
                            Console.WriteLine("Invalid Grade input. Must be a number.");
                            return;
                        }
                        students[i].Grade = grade;

                        Console.WriteLine("Student updated successfully.");
                        return;
                    }
                }

                Console.WriteLine("Student not found.");
            }

            private void DeleteStudent()
            {
                int id;
                while (true)
                {
                    Console.Write("Enter Student ID to delete (numbers only): ");
                    if (int.TryParse(Console.ReadLine(), out id)) break;
                    Console.WriteLine("Invalid ID. Please enter numeric values only.");
                }

                for (int i = 0; i < count; i++)
                {
                    if (students[i].ID == id)
                    {
                        for (int j = i; j < count - 1; j++)
                        {
                            students[j] = students[j + 1];
                        }
                        students[count - 1] = null;
                        count--;
                        Console.WriteLine("Student deleted successfully.");
                        return;
                    }
                }

                Console.WriteLine("Student not found.");
            }

            private bool IsAllLetters(string str)
            {
                foreach (char c in str)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        return false;
                    }
                }
                return true;
            }

            private string GetFullCourseName(string input)
            {
                foreach (var pair in validCourses)
                {
                    if (pair.Key.Equals(input, StringComparison.OrdinalIgnoreCase) ||
                        pair.Value.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        return pair.Value;
                    }
                }
                return null;
            }
        }
    }
}
