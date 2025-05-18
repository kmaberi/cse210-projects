using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Represents a scripture, including its reference and text.
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string GetDisplayText()
    {
        string wordsText = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        return $"{_reference.GetDisplayText()} - {wordsText}";
    }

    public int HideRandomWords(int count)
    {
        var visibleWords = _words.Where(word => !word.IsHidden()).ToList();

        if (visibleWords.Count == 0)
        {
            return 0; // All words are already hidden
        }

        Random random = new Random();
        int hiddenCount = 0;

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
            hiddenCount++;
        }

        return hiddenCount; // Return the number of words hidden
    }

    public int GetTotalWords()
    {
        return _words.Count;
    }

    public void SaveProgress(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(_reference.GetDisplayText());
            writer.WriteLine(string.Join(" ", _words.Select(word => word.IsHidden() ? "1" : "0")));
        }
    }

    public void LoadProgress(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length >= 2)
        {
            string[] hiddenStates = lines[1].Split(' ');
            for (int i = 0; i < _words.Count; i++)
            {
                if (hiddenStates[i] == "1")
                {
                    _words[i].Hide();
                }
            }
        }
    }
}

// Represents a single word in the scripture and manages its visibility.
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

// Represents the reference of a scripture (e.g., "John 3:16").
public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    // Constructor for a single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    // Constructor for a verse range
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        return _startVerse == _endVerse
            ? $"{_book} {_chapter}:{_startVerse}"
            : $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Load scriptures from file
        List<Scripture> scriptures = LoadScriptures("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found. Please check the scriptures.txt file.");
            return;
        }

        // Select a random scripture
        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];

        Console.WriteLine("Available Scriptures:");
        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptures[i].GetDisplayText()}");
        }

        Console.WriteLine("\nEnter the number of the scripture you want to memorize, or press Enter to select one randomly:");
        string choice = Console.ReadLine();

        if (int.TryParse(choice, out int index) && index > 0 && index <= scriptures.Count)
        {
            scripture = scriptures[index - 1];
        }
        else
        {
            Console.WriteLine("Invalid choice. Selecting a random scripture.");
            scripture = scriptures[rand.Next(scriptures.Count)];
        }

        int timeLimit = 60; // 60 seconds
        DateTime startTime = DateTime.Now;

        while (true)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            if (elapsed.TotalSeconds >= timeLimit)
            {
                Console.Clear();
                Console.WriteLine("Time's up! Here's the scripture:");
                Console.WriteLine(scripture.GetDisplayText());
                break;
            }

            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine($"\nTime remaining: {timeLimit - (int)elapsed.TotalSeconds} seconds.");
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            if (!scripture.HideRandomWords(3))
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden. Goodbye!");
                break;
            }
        }
    }

    static List<Scripture> LoadScriptures(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 5)
                {
                    Reference reference = new Reference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                    string text = parts[4];
                    scriptures.Add(new Scripture(reference, text));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scriptures from {filePath}: {ex.Message}");
        }

        return scriptures;
    }
}