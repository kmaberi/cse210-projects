using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static int score = 0;
    static List<Goal> goals = new List<Goal>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"You have {score} points.\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoals();
            else if (choice == "3") SaveGoals();
            else if (choice == "4") LoadGoals();
            else if (choice == "5") RecordEvent();
            else if (choice == "6") break;
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
            goals.Add(new SimpleGoal(name, desc, points));
        else if (type == "2")
            goals.Add(new EternalGoal(name, desc, points));
        else if (type == "3")
        {
            Console.Write("Enter target count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus: ");
            int bonus = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    static void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        int i = 1;
        foreach (Goal goal in goals)
        {
            Console.WriteLine($"{i}. {goal.GetStatus()} {goal.GetName()} ({goal.GetDescription()})");
            i++;
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }

    static void SaveGoals()
    {
        Console.Write("Enter filename: ");
        string filename = Console.ReadLine();
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(score);
            foreach (Goal goal in goals)
                outputFile.WriteLine(goal.GetStringRepresentation());
        }
        Console.WriteLine("Goals saved. Press enter to continue...");
        Console.ReadLine();
    }

    static void LoadGoals()
    {
        Console.Write("Enter filename: ");
        string filename = Console.ReadLine();
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found. Press enter to continue...");
            Console.ReadLine();
            return;
        }
        string[] lines = File.ReadAllLines(filename);
        goals.Clear();
        score = int.Parse(lines[0]);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string type = parts[0];
            string[] details = parts[1].Split(',');

            if (type == "SimpleGoal")
                goals.Add(new SimpleGoal(details[0], details[1], int.Parse(details[2]))); // isComplete not loaded for simplicity
            else if (type == "EternalGoal")
                goals.Add(new EternalGoal(details[0], details[1], int.Parse(details[2])));
            else if (type == "ChecklistGoal")
                goals.Add(new ChecklistGoal(details[0], details[1], int.Parse(details[2]), int.Parse(details[4]), int.Parse(details[3])));
        }
        Console.WriteLine("Goals loaded. Press enter to continue...");
        Console.ReadLine();
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index < 0 || index >= goals.Count)
        {
            Console.WriteLine("Invalid selection. Press enter to continue...");
            Console.ReadLine();
            return;
        }
        Goal goal = goals[index];
        goal.RecordEvent();
        score += goal.GetPoints();
        if (goal is ChecklistGoal cg && cg.IsComplete())
            score += cg.GetBonus();
        Console.WriteLine($"Event recorded! You now have {score} points. Press enter to continue...");
        Console.ReadLine();
    }
}