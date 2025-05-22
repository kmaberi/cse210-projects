// Program.cs
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
            // Example scripture: Proverbs 3:5–6
            Reference reference = new Reference("Proverbs", 3, 5, 6);
            string text = "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.";
            Scripture scripture = new Scripture(reference, text);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

                string input = Console.ReadLine();
                if (input != null && input.Trim().ToLower() == "quit")
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
    }

    // Reference.cs
    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int _endVerse;

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

    // Word.cs
    public class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden() => _isHidden;

        public void Hide() => _isHidden = true;

        public string GetDisplayText()
        {
            // Show underscores matching word length when hidden
            return _isHidden ? new string('_', _text.Length) : _text;
        }
    }

    // Scripture.cs
    using System.Collections.Generic;
    using System.Linq;

    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            // Split on spaces and preserve punctuation
            _words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        public string GetDisplayText()
        {
            string referenceText = _reference.GetDisplayText();
            string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{referenceText}\n{wordsText}";
        }

        // HideRandomWords returns false when no words left to hide
        public bool HideRandomWords(int count)
        {
            var visibleWords = _words.Where(w => !w.IsHidden()).ToList();
            if (visibleWords.Count == 0)
                return false;

            Random random = new Random();
            for (int i = 0; i < count && visibleWords.Count > 0; i++)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                visibleWords.RemoveAt(index);
            }

            return true;
        }
    }
}
