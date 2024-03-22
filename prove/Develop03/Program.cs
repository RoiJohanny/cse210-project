using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Creating a scripture
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world...");

        // Displaying the initial scripture
        Console.WriteLine("Press Enter to start:");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());

        // Hiding random words until all words are hidden
        while (!scripture.AllWordsHidden())
        {
            Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
        }

        Console.WriteLine("\nAll words in the scripture are now hidden. Press any key to exit.");
        Console.ReadKey();
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(string referenceText, string scriptureText)
    {
        reference = new Reference(referenceText);
        words = scriptureText.Split(' ').Select(word => new Word(word)).ToList();
    }

    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden);
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        int numWordsToHide = random.Next(1, words.Count / 2); // Hide up to half of the words
        int wordsHidden = 0;

        while (wordsHidden < numWordsToHide)
        {
            int index = random.Next(words.Count);
            if (!words[index].IsHidden)
            {
                words[index].Hide();
                wordsHidden++;
            }
        }
    }

    public string GetDisplayText()
    {
        return $"{reference.GetDisplayText()}\n\n{string.Join(" ", words.Select(word => word.GetDisplayText()))}";
    }
}

class Reference
{
    private string text;

    public Reference(string text)
    {
        this.text = text;
    }

    public string GetDisplayText()
    {
        return text;
    }
}

class Word
{
    private string text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string GetDisplayText()
    {
        return IsHidden ? "_____" : text;
    }
}