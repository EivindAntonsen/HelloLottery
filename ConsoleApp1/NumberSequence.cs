using System;
using System.Collections.Generic;

namespace Lottery
{
    internal abstract class NumberSequence
    {
        public abstract List<int> Numbers();
        protected const int Min = 1;
    }

    internal class Secondary : NumberSequence
    {
        private const int Max = 10;
        private const int Size = 2;
        private readonly List<int> numberSequence = new List<int>();
        private static readonly Random Random = new Random();

        public override List<int> Numbers()
        {
            return numberSequence;
        }

        public Secondary()
        {

            while (numberSequence.Count != Size)
            {
                var number = Random.Next(Min, Max);

                if (!numberSequence.Contains(number))
                {
                    numberSequence.Add(number);
                }
            }
        }
    }

    internal class Primary : NumberSequence
    {
        private const int Max = 10;
        private const int Size = 5;
        private readonly List<int> numberSequence = new List<int>();
        private static readonly Random Random = new Random();

        public override List<int> Numbers()
        {
            return numberSequence;
        }

        public Primary()
        {
            while (numberSequence.Count != Size)
            {
                var number = Random.Next(Min, Max);

                if (!numberSequence.Contains(number))
                {
                    numberSequence.Add(number);
                }
            }
        }
    }
}