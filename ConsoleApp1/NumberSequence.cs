using System;
using System.Collections.Generic;

namespace Lottery
{
    internal abstract class NumberSequence
    {
        public abstract List<int> Numbers();
    }

    internal class Secondary : NumberSequence
    {
        private readonly List<int> numberSequence = new List<int>();
        private static readonly Random Random = new Random();

        public override List<int> Numbers()
        {
            return numberSequence;
        }

        public Secondary()
        {
            while (numberSequence.Count != 2)
            {
                var number = Random.Next(1, 10);

                if (!numberSequence.Contains(number))
                {
                    numberSequence.Add(number);
                }
            }
        }
    }

    internal class Primary : NumberSequence
    {
        private readonly List<int> numberSequence = new List<int>();
        private static readonly Random Random = new Random();

        public override List<int> Numbers()
        {
            return numberSequence;
        }

        public Primary()
        {
            while (numberSequence.Count != 5)
            {
                var number = Random.Next(1, 50);

                if (!numberSequence.Contains(number))
                {
                    numberSequence.Add(number);
                }
            }
        }
    }
}