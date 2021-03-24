using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using FizzBuzz.Outputs;
using FizzBuzz.Rules;

namespace FizzBuzz.Engines
{
    public class DefaultFizzBuzzEngine : IFizzBuzzEngine
    {
        private readonly IEnumerable<IRule> rules;
        private readonly IOutput output;
        
        public DefaultFizzBuzzEngine(IEnumerable<IRule> rules, IOutput output)
        {
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            this.rules = rules;
            this.output = output;
        }

        public void Run(int limit = 100)
        {
            var builder = new StringBuilder(this.rules.Sum(x => x.MaxOutputSize));

            for (int i = 1; i <= limit; i++)
            {
                builder.Clear();

                foreach (var rule in this.rules)
                {
                    if (rule.CanHandle(i))
                    {
                        builder.Append(rule.Handle(i));
                    }
                }

                if (builder.Length < 1)
                {
                    builder.Append(i);
                }
                
                this.output.Write(i, builder.ToString());
            }
        }
    }
}