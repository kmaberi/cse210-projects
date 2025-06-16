using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>();

            // Create one of each activity.
            activities.Add(new Running(new DateTime(2022, 11, 03), 30, 3.0));    // Running: 3.0 miles in 30 min
            activities.Add(new Cycling(new DateTime(2022, 11, 03), 30, 12.0));   // Cycling: 12 mph for 30 min
            activities.Add(new Swimming(new DateTime(2022, 11, 03), 30, 20));    // Swimming: 20 laps in 30 min

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}