using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            if (choice == "1")
                new BreathingActivity().Start();
            else if (choice == "2")
                new ReflectionActivity().Start();
            else if (choice == "3")
                new ListingActivity().Start();
            else if (choice == "4")
                break;
            else
            {
                Console.WriteLine("Invalid input. Press enter to continue.");
                Console.ReadLine();
            }
        }
    }
}
