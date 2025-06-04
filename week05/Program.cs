using System;

class Program
{
    static void Main(string[] args)
    {
        int breathingCount = 0;
        int reflectionCount = 0;
        int listingCount = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Show Session Log");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                new BreathingActivity().Start();
                breathingCount++;
            }
            else if (choice == "2")
            {
                new ReflectionActivity().Start();
                reflectionCount++;
            }
            else if (choice == "3")
            {
                new ListingActivity().Start();
                listingCount++;
            }
            else if (choice == "4")
            {
                Console.WriteLine($"\nSession Log:");
                Console.WriteLine($"Breathing Activity: {breathingCount} times");
                Console.WriteLine($"Reflection Activity: {reflectionCount} times");
                Console.WriteLine($"Listing Activity: {listingCount} times");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            else if (choice == "5")
                break;
            else
            {
                Console.WriteLine("Invalid input. Press enter to continue.");
                Console.ReadLine();
            }
        }
    }
}
