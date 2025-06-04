public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    protected override void PerformActivity()
    {
        int elapsed = 0;
        while (elapsed < _duration)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(3);
            elapsed += 3;
            if (elapsed >= _duration) break;
            Console.Write("Breathe out... ");
            ShowCountdown(3);
            elapsed += 3;
            Console.WriteLine();
        }
    }
}