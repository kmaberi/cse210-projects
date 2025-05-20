using System;
using System.Collections.Generic;
using System.Linq;
using ScriptureMemorizer;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example scripture: Proverbs 3:5-6
            Reference reference = new Reference("Proverbs", 3, 5, 6);
            string text = "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.";
            Scripture scripture = new Scripture(reference, text);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
                }

                if (scripture.HideRandomWords(3) == 0)
                {
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine("\nAll words are hidden. Goodbye!");
                    break;
                }
            }
        }
    }

    public class Reference
    {
        // Class implementation
    }

    public class Scripture
    {
        private List<Word> _words = new List<Word>();

        public Scripture(Reference reference, string text)
        {
            // Assume this constructor is implemented
        }

        public string GetDisplayText()
        {
            // Assume this method is implemented
        }

        public int HideRandomWords(int count)
        {
            var visibleWords = _words.Where(word => !word.IsHidden()).ToList();

            if (visibleWords.Count == 0)
            {
                return 0; // No words left to hide
            }

            Random random = new Random();
            for (int i = 0; i < count && visibleWords.Count > 0; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }

            return 1; // Words were hidden
        }
    }

    public class Word
    {
        // Class implementation
    }
}