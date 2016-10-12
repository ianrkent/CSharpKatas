using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.CSharp.Tests.StringCalculator
{
    public class ProblemSpec
    {
        public ProblemSpec(string input)
        {
            DetermineOperands(input);
        }

        public string[] Operands { get; set; }


        private void DetermineOperands(string input)
        {
            IEnumerable<string> delimeters;
            string problem;

            if (UsesCustomDelimeter(input))
            {
                var firstsNewLineIndex = input.IndexOf('\n');
                var delimeterSpecification = input.Substring(2, firstsNewLineIndex - 2);

                delimeters = ParseDelimeterSpecification(delimeterSpecification);
                problem = input.Substring(firstsNewLineIndex + 1);
            }
            else
            {
                problem = input;
                delimeters = new[] {","};
            }

            Operands = problem
                .Split(delimeters.Union(new[] {"\n"}).ToArray(), StringSplitOptions.None);
        }

        private static IEnumerable<string> ParseDelimeterSpecification(string delimeterSpecification)
        {
            if (!delimeterSpecification.Contains("["))
            {
                return new[] { delimeterSpecification };
            }

            return delimeterSpecification
                .Replace("][", "[")
                .Replace("]", "[")
                .Split('[');
        }

        private static bool UsesCustomDelimeter(string problem)
        {
            return problem.Substring(0, 2) == "//";
        }
    }
}