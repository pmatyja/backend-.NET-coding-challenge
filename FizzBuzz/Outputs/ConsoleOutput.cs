using System;

namespace FizzBuzz.Outputs
{
    public class ConsoleOutput : IOutput
    {
        public void Write(int value, string input)
        {
            Console.WriteLine("{0}: {1}", value, input);
        }
    }
}