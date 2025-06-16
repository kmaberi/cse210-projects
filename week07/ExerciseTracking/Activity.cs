using System;

namespace ExerciseTracking
{
    public abstract class Activity
    {
        private DateTime _date;
        private int _minutes;

        public Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        public DateTime Date => _date;
        public int Minutes => _minutes;

        // Abstract methods for each derived class to override.
        public abstract double GetDistance(); // in miles
        public abstract double GetSpeed();    // in mph
        public abstract double GetPace();     // in minutes per mile

        // Marked as virtual so derived classes can override if needed.
        public virtual string GetSummary()
        {
            return $"{_date:dd MMM yyyy} {this.GetType().Name} ({_minutes} min) - Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
        }
    }
}