using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            var prizeDictionary = CreatePrizeDictionary();
            const int jackpotReward = 900000000;
            const int ticketPrice = 25;
            var stopwatch = new Stopwatch();
            var winningTicket = new Ticket();
            var wonJackpot = false;
            var myPrizes = new List<int>();
            var wallet = 0;
            var count = 0;

            stopwatch.Start();

            while (!wonJackpot)
            {
                var myTicket = new Ticket();
                wallet -= ticketPrice;
                count += 1;

                var myPrize = CalculatePrize(myTicket, winningTicket, prizeDictionary);

                if (myPrize > 0)
                {
                    myPrizes.Add(myPrize);
                    if (myPrize == 1) wonJackpot = true;
                }

                if (count % 3000000 == 0)
                {
                    Console.WriteLine("Tickets bought: {0}...", count);
                }
            }

            var prizeDistribution = myPrizes.GroupBy(prize => prize)
                .Select(eachPrize => Tuple.Create(eachPrize.Key, eachPrize.Count()))
                .ToDictionary(x => x.Item1, x => x.Item2);

            stopwatch.Stop();
            var runtime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("Lottery won after {0} tickets bought for {1} money spent in {2}ms..",
                count,
                wallet.ToString("C", CultureInfo.CurrentCulture),
                runtime.ToString()
            );

            var totalPrizes = prizeDistribution.Values.Sum();

            foreach (var (prize, amountOfPrize) in prizeDistribution)
            {
                Console.WriteLine("prize: {0}, {1} ({2})",
                    prize.ToString(),
                    ((double) amountOfPrize / totalPrizes).ToString("P4", CultureInfo.CurrentCulture),
                    amountOfPrize.ToString()
                );
            }

            wallet += jackpotReward;

            Console.WriteLine("Sum after gambling: {0}",
                wallet.ToString("C", CultureInfo.CurrentCulture)
            );
        }

        private static Dictionary<Tuple<int, int>, int> CreatePrizeDictionary()
        {
            return new Dictionary<Tuple<int, int>, int>
            {
                {Tuple.Create(5, 2), 1},
                {Tuple.Create(5, 1), 2},
                {Tuple.Create(5, 0), 3},
                {Tuple.Create(4, 2), 4},
                {Tuple.Create(4, 1), 5},
                {Tuple.Create(4, 0), 6},
                {Tuple.Create(3, 2), 7},
                {Tuple.Create(2, 2), 8},
                {Tuple.Create(3, 1), 9},
                {Tuple.Create(3, 0), 10},
                {Tuple.Create(1, 2), 11},
                {Tuple.Create(2, 1), 12}
            };
        }

        private static int CalculatePrize(Ticket ticket, Ticket winningTicket,
            IReadOnlyDictionary<Tuple<int, int>, int> prizeDictionary)
        {
            var matchingPrimaries = ticket.PrimarySequence().FindAll(number =>
                winningTicket.PrimarySequence().Contains(number)
            ).Count;

            if (matchingPrimaries == 0) return 0;

            var matchingSecondaries = ticket.SecondarySequence().FindAll(number =>
                winningTicket.SecondarySequence().Contains(number)
            ).Count;

            switch (matchingPrimaries)
            {
                case 1 when matchingSecondaries < 2:
                case 2 when matchingSecondaries < 1:
                    return 0;
                default:
                    return prizeDictionary.GetValueOrDefault(Tuple.Create(matchingPrimaries, matchingSecondaries));
            }
        }

        private static bool TicketIsJackpot(Ticket ticket, Ticket winningTicket)
        {
            if (ticket.SecondarySequence().Any(number =>
                !winningTicket.SecondarySequence().Contains(number))
            ) return false;

            return ticket.PrimarySequence().All(primaryNumber =>
                winningTicket.PrimarySequence().Contains(primaryNumber)
            );
        }
    }
}