using System;

namespace ExerciseTracking
{
    public class Swimming : Activity
    {
        private int _laps;

        public Swimming(DateTime date, int minutes, int laps)
            : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            // Each lap is 50 meters.
            // Convert to miles: (laps * 50 / 1000) km * 0.62 ≈ miles.
            return _laps * 50.0 / 1000 * 0.62;
        }

        public override double GetSpeed()
        {
            return (GetDistance() / Minutes) * 60;
        }

        public override double GetPace()
        {
            return Minutes / GetDistance();
        }

        //public override string GetSummary() => base.GetSummary();
    }
}