using System;


   public class Program
{
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nJournal App");
            Console.WriteLine("1. New Entry");
            Console.WriteLine("2. Display Journal");
            Console.WriteLine("3. Save Journal");
            Console.WriteLine("4. Load Journal");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToFile();
                    break;
                case "4":
                    journal.LoadFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

public class Journal
{
    private List<Entry> entries = new List<Entry>();
    private string[] prompts = {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "What am I grateful for today?",
        "What did I learn today?",
        "If I had one thing I could do differently today, what would it be?"
    };

    public void AddEntry()
    {
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);
        string content = Console.ReadLine();

        Entry entry = new Entry(DateTime.Now, prompt, content);
        entries.Add(entry);

        Console.WriteLine("Entry added successfully!");
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("There are no entries in your journal.");
            return;
        }

        foreach (Entry entry in entries)
        {
            Console.WriteLine($"\n--- Entry ---");
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Content: {entry.Content}");
        }
    }

    public void SaveToFile()
    {
        Console.WriteLine("Enter filename to save journal:");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    writer.WriteLine(entry.ToString());
                }
            }
            Console.WriteLine("Journal saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        Console.WriteLine("Enter filename to load journal:");
        string filename = Console.ReadLine();

        try
        {
            entries.Clear(); // Clear current entries before loading
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Entry entry = Entry.FromString(line);
                    entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }
}

public class Entry
{
    public DateTime Date { get; set; }
    public string Prompt { get; set; }
    public string Content { get; set; }

    public Entry(DateTime date, string prompt, string content)
    {
        Date = date;
        Prompt = prompt;
        Content = content;
    }

    public override string ToString()
    {
        return $"{Date},{Prompt},{Content}";
    }

    public static Entry FromString(string data)
    {
        string[] parts = data.Split(',');
        return new Entry(DateTime.Parse(parts[0]), parts[1], parts[2]);
    }
}

