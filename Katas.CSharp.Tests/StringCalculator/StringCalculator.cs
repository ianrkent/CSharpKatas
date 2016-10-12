using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.CSharp.Tests.StringCalculator
{
    public class StringCalculator
    {
        public int Add(string problem)
        {
            if (string.IsNullOrWhiteSpace(problem))
            {
                return 0;
            }

            var problemSpecification = new ProblemSpec(problem);

            var operands = problemSpecification.Operands
                .Select(int.Parse)
                .ToList();

            EnsureOnlyPositiveOperands(operands);

            return operands
                .Where(operand => operand <= 1000)
                .Sum();
        }

        private static void EnsureOnlyPositiveOperands(IEnumerable<int> operands)
        {
            var negativeOperands = operands.Where(operand => operand < 0).ToList();
            if (negativeOperands.Any())
            {
                throw new Exception($"negatives not allowed:  {string.Join(", ", negativeOperands)}");
            }
        }
    }
}