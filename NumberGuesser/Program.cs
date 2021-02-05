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
                    var userTask = TelemetryService.DefaultSession.StartUserTask("vs/guesser/guess-round");
                    userTask.EndEvent.Properties.Add("vs.guesser.number", number);
                    int guesses = 0;

                    while (true)
                    {
                        Console.WriteLine($"Guess a number between 1-100");
                        int guess = int.Parse(Console.ReadLine());
                        guesses++;

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

                            try
                            {
                                if (Console.ReadLine().ToLowerInvariant() == "y")
                                {
                                    break;
                                }
                            }
                            catch(Exception ex)
                            {
                                _ = TelemetryService.DefaultSession.PostFault("vs/guesser/input-fault", "bad inputs", ex);
                            }

                            var e = new TelemetryEvent("vs/guesser/myevent");
                            e.Properties.Add("vstelworkbench.number", 42);
                            e.Properties.Add("vstelworkbench.string", "helloworld");
                            TelemetryService.DefaultSession.PostEvent(e);
                            return;
                        }
                    }
                    userTask.EndEvent.Properties.Add("vs.guesser.guesses", guesses);
                    userTask.End(TelemetryResult.Success);
                }
            }
        }
    }
}
