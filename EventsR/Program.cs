using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CollegeClassModel history = new CollegeClassModel("History 202", 2);
            CollegeClassModel programming = new CollegeClassModel("Javascript 303", 2);

            history.EnrollementFull += History_EnrollementFull;
            programming.EnrollementFull += Programming_EnrollementFull;

            history.SignedUpStudent("John1").PrintToConsole();
            history.SignedUpStudent("Alfred").PrintToConsole();
            history.SignedUpStudent("Johnny").PrintToConsole();
            history.SignedUpStudent("Mikhail").PrintToConsole();


            programming.SignedUpStudent("Gereny").PrintToConsole();
            programming.SignedUpStudent("Johnny").PrintToConsole();
            programming.SignedUpStudent("Mikhail").PrintToConsole();
            programming.SignedUpStudent("John1").PrintToConsole();
            programming.SignedUpStudent("Alfred").PrintToConsole();

            Console.ReadLine();
        }

        private static void Programming_EnrollementFull(object sender, string e)
        {
            CollegeClassModel model = (CollegeClassModel)sender;
            Console.WriteLine();
            Console.WriteLine($"{model.CourseTitle}: Full");
            Console.WriteLine();
        }

        private static void History_EnrollementFull(object sender, string e)
        {
            CollegeClassModel model = (CollegeClassModel)sender;
            Console.WriteLine();
            Console.WriteLine($"{model.CourseTitle}: Full");
            Console.WriteLine();
        }
    }

    public static class ConsoleHelpers
    {
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }
    }

    public class CollegeClassModel
    {
        public event EventHandler<string> EnrollementFull;

        private List<string> enrolledStudents = new List<string>();
        private List<string> waitingList = new List<string>();

        public string CourseTitle { get; set; }
        public int MaximumStudents { get; set; }

        public CollegeClassModel(string title, int maximumStudents)
        {
            CourseTitle = title;
            MaximumStudents = maximumStudents;
        }

        public string SignedUpStudent(string studentName)
        {
            string output = "";
            if (enrolledStudents.Count < MaximumStudents)
            {
                enrolledStudents.Add(studentName);
                output = $"{studentName} was enrolled in {CourseTitle}";
                // Check to see if we are maxed out
                if (enrolledStudents.Count == MaximumStudents)
                {
                    EnrollementFull?.Invoke(this, $"{CourseTitle} enrollment is now full.");
                }
            }
            else
            {
                waitingList.Add(studentName);

                output = $"{studentName} was added to the wait list in {CourseTitle}";
            }
            return output;
        }
    }
}
