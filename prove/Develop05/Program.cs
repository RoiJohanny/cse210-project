using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EternalQuest
{
    class Goal
    {
        public string Name { get; set; }
        public int PointsPerCompletion { get; set; }
        public int RequiredCompletions { get; set; }
        public int BonusPoints { get; set; }
        public int CurrentCompletions { get; set; }
        public bool IsCompleted => CurrentCompletions >= RequiredCompletions;

        public Goal(string name, int pointsPerCompletion, int requiredCompletions, int bonusPoints = 0)
        {
            Name = name;
            PointsPerCompletion = pointsPerCompletion;
            RequiredCompletions = requiredCompletions;
            BonusPoints = bonusPoints;
        }
    }

    class Program
    {
        private static List<Goal> goals = new List<Goal>();
        private static int totalPoints = 0;

        static void Main(string[] args)
        {
            LoadGoals();
            DisplayMenu();
        }

        static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\nEternal Quest Menu:");
                Console.WriteLine("1. View Goals");
                Console.WriteLine("2. Add New Goal");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. View Score");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewGoals();
                        break;
                    case "2":
                        AddGoal();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        ViewScore();
                        break;
                    case "5":
                        SaveGoals();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ViewGoals()
        {
            Console.WriteLine("\nYour Goals:");
            foreach (var goal in goals)
            {
                string status = goal.IsCompleted ? "[X]" : "[ ]";
                string progress = goal.RequiredCompletions > 1 ? $" (Completed {goal.CurrentCompletions}/{goal.RequiredCompletions} times)" : "";
                Console.WriteLine($"{status} {goal.Name}{progress}");
            }
        }

        static void AddGoal()
        {
            Console.Write("\nEnter goal name: ");
            string name = Console.ReadLine();

            Console.Write("Enter points per completion: ");
            int pointsPerCompletion = int.Parse(Console.ReadLine());

            Console.Write("Enter required completions (Enter 0 for eternal goal): ");
            int requiredCompletions = int.Parse(Console.ReadLine());

            Console.Write("Enter bonus points (0 if none): ");
            int bonusPoints = int.Parse(Console.ReadLine());

            Goal goal = new Goal(name, pointsPerCompletion, requiredCompletions, bonusPoints);
            goals.Add(goal);
            Console.WriteLine("Goal added successfully.");
        }

        static void RecordEvent()
        {
            Console.WriteLine("\nSelect a goal to record event:");

            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].Name}");
            }

            Console.Write("Choose a goal: ");
            int choice = int.Parse(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < goals.Count)
            {
                Goal goal = goals[choice];
                goal.CurrentCompletions++;

                totalPoints += goal.PointsPerCompletion;
                if (goal.IsCompleted && goal.BonusPoints > 0)
                {
                    totalPoints += goal.BonusPoints;
                }

                Console.WriteLine("Event recorded successfully.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void ViewScore()
        {
            Console.WriteLine($"\nYour Total Score: {totalPoints}");
        }

        static void LoadGoals()
        {
            try
            {
                if (File.Exists("goals.txt"))
                {
                    string[] lines = File.ReadAllLines("goals.txt");
                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(',');
                        Goal goal = new Goal(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                        goal.CurrentCompletions = int.Parse(parts[4]);
                        goals.Add(goal);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading goals: {ex.Message}");
            }
        }

        static void SaveGoals()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("goals.txt"))
                {
                    foreach (var goal in goals)
                    {
                        writer.WriteLine($"{goal.Name},{goal.PointsPerCompletion},{goal.RequiredCompletions},{goal.BonusPoints},{goal.CurrentCompletions}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving goals: {ex.Message}");
            }
        }
    }
}
