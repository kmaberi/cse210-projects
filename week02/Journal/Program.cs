using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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

    public int MoodRating => _moodRating; // Expose MoodRating for analysis
}

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    // Adds a new entry to the journal
    public void AddEntry(string prompt, string response, string tags, int moodRating)
    {
        _entries.Add(new Entry(prompt, response, tags, moodRating));
    }

    // Displays all entries in the journal
    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries found.");
            return;
        }

        foreach (var entry in _entries)
        {
            entry.Display();
        }
    }

    // Saves all entries to a file
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine(entry.ToFileString());
            }
        }
    }

    // Loads entries from a file
    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 5)
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];
                string tags = parts[3];
                int moodRating = int.Parse(parts[4]);

                _entries.Add(new Entry(date, prompt, response, tags, moodRating));
            }
        }
    }

    // Searches for entries containing the specified tag or keyword
    public void SearchEntries(string query)
    {
        var foundEntries = _entries.FindAll(e => e.ToFileString().Contains(query, StringComparison.OrdinalIgnoreCase));

        if (foundEntries.Count == 0)
        {
            Console.WriteLine("No entries found containing that tag or keyword.");
            return;
        }

        Console.WriteLine($"\nEntries containing '{query}':");
        foreach (var entry in foundEntries)
        {
            entry.Display();
        }
    }

    // Exports the journal entries to a JSON file
    public void ExportToJson(string filename)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(filename, jsonString);
    }

    // Displays a simple mood analysis based on the entries
    public void DisplayMoodAnalysis()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries to analyze.");
            return;
        }

        double averageMood = 0;
        foreach (var entry in _entries)
        {
            averageMood += entry.MoodRating;
        }
        averageMood /= _entries.Count;

        Console.WriteLine($"\nAverage Mood Rating: {averageMood:F2}");
        Console.WriteLine("Mood Analysis: " + (averageMood >= 7 ? "Positive" : averageMood >= 4 ? "Neutral" : "Negative"));
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
            Console.WriteLine("5. Search journal entries by tag or keyword");
            Console.WriteLine("6. Export journal to JSON");
            Console.WriteLine("7. View mood analysis");
            Console.WriteLine("8. Quit");
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
                    Console.Write("Enter a tag or keyword to search: ");
                    string searchQuery = Console.ReadLine();
                    journal.SearchEntries(searchQuery);
                    break;

                case "6":
                    Console.Write("Enter filename to export to JSON: ");
                    string jsonFilename = Console.ReadLine();
                    journal.ExportToJson(jsonFilename);
                    Console.WriteLine("Journal exported to JSON.");
                    break;

                case "7":
                    journal.DisplayMoodAnalysis();
                    break;

                case "8":
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