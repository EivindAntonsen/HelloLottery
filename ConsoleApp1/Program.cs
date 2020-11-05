using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            const int jackpotReward = 900000000;
            const int ticketPrice = 25;
            var stopwatch = new Stopwatch();
            var winningTicket = new Ticket();
            var hasWon = false;
            var wallet = 0;
            var count = 0;

            stopwatch.Start();

            while (!hasWon)
            {
                var myTicket = new Ticket();
                wallet -= ticketPrice;
                count += 1;

                if (TicketIsJackpot(myTicket, winningTicket))
                {
                    hasWon = true;
                }

                if (count % 1000000 == 0)
                {
                    Console.WriteLine("Count: {0}...", count);
                }
            }

            stopwatch.Stop();
            var runtime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("Lottery won after {0} tickets bought for {1} money spent in {2}ms..",
                count,
                wallet.ToString("C", CultureInfo.CurrentCulture),
                runtime.ToString()
            );

            wallet += jackpotReward;

            Console.WriteLine("Sum after gambling: {0}",
                wallet.ToString("C", CultureInfo.CurrentCulture)
            );
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