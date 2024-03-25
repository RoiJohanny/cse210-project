using System;
using System.Threading;

// Base class for all activities
public abstract class Activity
{
    protected int duration;
    
    public abstract void Start();
    
    public Activity(int duration)
    {
        this.duration = duration;
    }
    
    protected void CommonStart(string name, string description)
    {
        Console.WriteLine($"Starting {name} activity...");
        Console.WriteLine(description);
        Console.WriteLine($"Duration set to {duration} seconds.");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000); // Pause for 3 seconds
    }
    
    protected void CommonEnd(string name)
    {
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        Thread.Sleep(3000); // Pause for 3 seconds
    }
}

// Breathing Activity
public class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration) { }
    
    public override void Start()
    {
        CommonStart("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        
        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(2000); // Pause for 2 seconds
            Console.WriteLine("Breathe out...");
            Thread.Sleep(2000); // Pause for 2 seconds
        }
        
        CommonEnd("Breathing");
    }
}

// Reflection Activity
public class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    
    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
    
    public ReflectionActivity(int duration) : base(duration) { }
    
    public override void Start()
    {
        CommonStart("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        
        Random rnd = new Random();
        string prompt = prompts[rnd.Next(prompts.Length)];
        Console.WriteLine(prompt);
        Thread.Sleep(3000); // Pause for 3 seconds
        
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for 3 seconds
        }
        
        CommonEnd("Reflection");
    }
}

// Listing Activity
public class ListingActivity : Activity
{
    private string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };
    
    public ListingActivity(int duration) : base(duration) { }
    
    public override void Start()
    {
        CommonStart("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        
        Random rnd = new Random();
        string prompt = listingPrompts[rnd.Next(listingPrompts.Length)];
        Console.WriteLine(prompt);
        Thread.Sleep(3000); // Pause for 3 seconds
        
        Console.WriteLine("Begin listing items...");
        Thread.Sleep(duration * 1000); // Pause for the specified duration
        
        Console.WriteLine("Listed items: " + duration); // Display the number of items listed
        CommonEnd("Listing");
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Select an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Exit");
            
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 4)
                break;
                
            Console.Write("Enter duration (in seconds): ");
            int duration = int.Parse(Console.ReadLine());
            
            Activity activity;
            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity(duration);
                    break;
                case 2:
                    activity = new ReflectionActivity(duration);
                    break;
                case 3:
                    activity = new ListingActivity(duration);
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    continue;
            }
            
            activity.Start();
        }
    }
}
