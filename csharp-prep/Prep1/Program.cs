using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep1 World!");
        Console.WriteLine("what is your first name:");
        string First = Console.ReadLine();
        Console.WriteLine("what is your last name:");
        string Last  = Console.ReadLine();
        Console.WriteLine($"your name is {Last}, {First} {Last}");
    }
}