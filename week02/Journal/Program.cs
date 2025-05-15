using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    private string _prompt;
    private string _response;
    private string _date;
    private string _tags;
    private int _moodRating;

    // Constructor for new entries
    public Entry(string prompt, string response, string tags, int moodRating)
    {
        _prompt = prompt;
        _response = response;
        _tags = tags;
        _moodRating = moodRating;
        _date = DateTime.Now.ToShortDateString();
    }

    // Constructor for loading entries from a file
    public Entry(string date, string prompt, string response, string tags, int moodRating)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        _tags = tags;
        _moodRating = moodRating;
    }

    // Formats this entry as a line suitable for saving
    public string ToFileString()
    {
        return $"{_date}|{_prompt}|{_response}|{_tags}|{_moodRating}";
    }

    // Displays this entry to the console
    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine($"Tags: {_tags}");
        Console.WriteLine($"Mood Rating: {_moodRating}");
        Console.WriteLine(new string('-', 40));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        CheckReminder();

        while (true)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = prompts[new Random().Next(prompts.Count)];
                    Console.WriteLine($"\nPrompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    Console.Write("Tags (comma-separated): ");
                    string tags = Console.ReadLine();
                    Console.Write("Mood Rating (1-10): ");
                    int moodRating = int.Parse(Console.ReadLine());
                    journal.AddEntry(prompt, response, tags, moodRating);
                    break;

                case "2":
                    Console.WriteLine("\nJournal Entries:");
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter filename to save to: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    Console.WriteLine("Journal saved.");
                    break;

                case "4":
                    Console.Write("Enter filename to load from: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    Console.WriteLine("Journal loaded.");
                    break;

                case "5":
                    UpdateReminder();
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CheckReminder()
    {
        string reminderFile = "last_run.txt";
        if (File.Exists(reminderFile))
        {
            string lastRun = File.ReadAllText(reminderFile);
            DateTime lastRunDate = DateTime.Parse(lastRun);
            if ((DateTime.Now - lastRunDate).TotalDays >= 1)
            {
                Console.WriteLine("Reminder: It's been more than a day since your last journal entry!");
            }
        }
    }

    static void UpdateReminder()
    {
        string reminderFile = "last_run.txt";
        File.WriteAllText(reminderFile, DateTime.Now.ToString());
    }
}