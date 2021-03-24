using System;

namespace FizzBuzz.Rules
{
    public class DivisableBy : IRule
    {
        public int MaxOutpuSize { get; }

        private readonly string output;
        private readonly int divisableBy;

        public DivisableBy(string output, int divisableBy)
        {
            if (string.IsNullOrWhiteSpace(output))
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (divisableBy == 0)
            {
                throw new ArgumentException("Division by 0 will raise an exception", nameof(divisableBy));
            }

            this.MaxOutpuSize = output.Length;
            this.output = output;
            this.divisableBy = divisableBy;
        }

        public bool CanHandle(int value)
        {
            return value % this.divisableBy == 0;
        }

        public string Handle(int value)
        {
            return this.output;
        }
    }
}
