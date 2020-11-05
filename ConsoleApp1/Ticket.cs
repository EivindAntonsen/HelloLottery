using System.Collections.Generic;

namespace Lottery
{
    internal class Ticket
    {
        private readonly Primary primarySequence = new Primary();
        private readonly Secondary secondarySequence = new Secondary();

        public List<int> PrimarySequence()
        {
            return primarySequence.Numbers();
        }

        public List<int> SecondarySequence()
        {
            return secondarySequence.Numbers();
        }
    }
}