using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your numeric grade: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int grade))
        {
            string letterGrade;
            string sign = "";

            // Determine the letter grade
            if (grade >= 90 && grade <= 100)
            {
                letterGrade = "A";
            }
            else if (grade >= 80)
            {
                letterGrade = "B";
            }
            else if (grade >= 70)
            {
                letterGrade = "C";
            }
            else if (grade >= 60)
            {
                letterGrade = "D";
            }
            else if (grade >= 0)
            {
                letterGrade = "F";
            }
            else
            {
                Console.WriteLine("Invalid grade. Please enter a grade between 0 and 100.");
                return;
            }

            // Determine the sign (+ or -)
            if (letterGrade != "A" && letterGrade != "F")
            {
                int lastDigit = grade % 10;

                if (lastDigit >= 7)
                {
                    sign = "+";
                }
                else if (lastDigit < 3)
                {
                    sign = "-";
                }
            }

            // Display the letter grade with the sign
            Console.WriteLine($"Your letter grade is: {letterGrade}{sign}");

            // Check if the user passed or failed
            if (grade >= 70)
            {
                Console.WriteLine("Congratulations! You passed the course.");
            }
            else
            {
                Console.WriteLine("Don't give up! Keep trying, and you'll do better next time.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a numeric grade.");
        }
    }
}