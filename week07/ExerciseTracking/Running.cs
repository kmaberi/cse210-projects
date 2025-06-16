using System;

namespace ExerciseTracking
{
    public class Running : Activity
    {
        private double _distance; // distance in miles

        public Running(DateTime date, int minutes, double distance)
            : base(date, minutes)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;

        public override double GetSpeed()
        {
            // Speed = (distance / minutes) * 60
            return (_distance / Minutes) * 60;
        }

        public override double GetPace()
        {
            // Pace = minutes / distance
            return Minutes / _distance;
        }

        // If you do not need a custom summary, you can remove this override.
        // Otherwise, keep it if you want to change formatting.
        //public override string GetSummary() => base.GetSummary();
    }
}