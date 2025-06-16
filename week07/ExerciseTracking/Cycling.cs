using System;

namespace ExerciseTracking
{
    public class Cycling : Activity
    {
        private double _speed; // provided speed in mph

        public Cycling(DateTime date, int minutes, double speed)
            : base(date, minutes)
        {
            _speed = speed;
        }

        public override double GetDistance()
        {
            // Distance = speed * (minutes/60)
            return (_speed * Minutes) / 60;
        }

        public override double GetSpeed() => _speed;

        public override double GetPace()
        {
            // Pace = minutes per mile = minutes / distance
            return Minutes / GetDistance();
        }

        //public override string GetSummary() => base.GetSummary();
    }
}