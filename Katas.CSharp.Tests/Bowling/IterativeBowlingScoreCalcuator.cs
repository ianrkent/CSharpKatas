using System;
using System.Linq;

namespace Katas.CSharp.Tests.Bowling
{
    public class IterativeBowlingScoreCalcuator  
    {
        public int CalculateScoreIteratively(string scoreCard)
        {
            var scoreSections = scoreCard.Split(new string[] {"||"}, StringSplitOptions.None);
            var normalTurns = scoreSections[0].Replace("|", string.Empty).ToCharArray(); ;
            var extraTurns = scoreSections[1];

            normalTurns = scoreCard.Replace("|", string.Empty).ToCharArray();


            var multiplyBy = Enumerable.Repeat(1, normalTurns.Length).ToArray();

            for (var i = 0; i < normalTurns.Length; i++)
            {
                if (normalTurns[i] == 'X')
                {
                    SafeArrayElementIncrement(multiplyBy, i + 1);
                    SafeArrayElementIncrement(multiplyBy, i + 2);
                }

                if (normalTurns[i] == '/')
                {
                    SafeArrayElementIncrement(multiplyBy, i + 1);
                }
            }

            var normalTurnValues = normalTurns
                .Select((rollResult, rollIndex) => PinScoreToValue(rollResult, rollIndex > 0 ? normalTurns[rollIndex - 1] : '-'))
                .ToArray();

            return 
                normalTurnValues
                .Select((normalTurnVallue, i) => normalTurnVallue * multiplyBy[i])
                .Sum() + extraTurns.Select(rollResult => PinScoreToValue(rollResult)).Sum();

        }

        private static void SafeArrayElementIncrement(int[] array, int i)
        {
            if (i >= 0 && i < array.Length) array[i]++;
        }

        private static int PinScoreToValue(char pinScore, char previousPinScore = '-')
        {
            if (pinScore == '-') return 0;
            if (pinScore == 'X') return 10;
            if (pinScore == '/') return 10 - int.Parse(previousPinScore.ToString());

            return int.Parse(pinScore.ToString());
        }
    }
}