using Microsoft.VisualStudio.Telemetry;
using System;
using VSTelWorkbench;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new FileLoggingTelemetrySession())
            {
                Random r = new Random();

                while (true)
                {
                    int number = r.Next(100);
                    while (true)
                    {
                        Console.WriteLine($"Guess a number between 1-100");
                        int guess = int.Parse(Console.ReadLine());
                        if (guess < number)
                        {
                            Console.WriteLine("Pick a higher number");
                        }
                        else if (guess > number)
                        {
                            Console.WriteLine("Pick a smaller number");
                        }
                        else
                        {
                            Console.WriteLine("You guessed it! Play again?");
                            if (Console.ReadLine().ToLowerInvariant() == "y")
                            {
                                break;
                            }

                            TelemetryService.DefaultSession.PostEvent("vs/numberguesser/alldone");
                            return;
                        }
                    }
                }
            }
        }
    }
}
