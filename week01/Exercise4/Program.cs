using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Number Analyzer!");
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        Console.WriteLine("Let's see what interesting facts we can find about your numbers!\n");

        List<int> numbers = new List<int>();

        // Collect numbers from the user
        while (true)
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                if (number == 0)
                {
                    break;
                }
                numbers.Add(number);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        if (numbers.Count == 0)
        {
            Console.WriteLine("You didn't enter any numbers. Goodbye!");
            return;
        }

        // Core Requirements
        // Compute the sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        Console.WriteLine($"\nThe sum of your numbers is: {sum}");

        // Compute the average
        double average = (double)sum / numbers.Count;
        Console.WriteLine($"The average of your numbers is: {average:F2}");

        // Find the maximum number
        int max = int.MinValue;
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenges
        // Find the smallest positive number
        int smallestPositive = int.MaxValue;
        foreach (int number in numbers)
        {
            if (number > 0 && number < smallestPositive)
            {
                smallestPositive = number;
            }
        }
        if (smallestPositive != int.MaxValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers were entered.");
        }

        // Sort the list and display it
        numbers.Sort();
        Console.WriteLine("\nThe sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        // Summary
        Console.WriteLine("\n--- Summary ---");
        Console.WriteLine($"You entered {numbers.Count} numbers.");
        Console.WriteLine($"Sum: {sum}, Average: {average:F2}, Largest: {max}");
        if (smallestPositive != int.MaxValue)
        {
            Console.WriteLine($"Smallest Positive: {smallestPositive}");
        }
        Console.WriteLine("Thanks for using the Number Analyzer! Have a great day!");
    }
}