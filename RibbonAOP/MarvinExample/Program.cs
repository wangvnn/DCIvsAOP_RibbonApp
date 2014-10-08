using Marvin.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvinExample 
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var list = new List<LedgerEntry>();
                list.Add(new LedgerEntry("start", 0m));
                list.Add(new LedgerEntry("first deposit", 1000m));
                var source = new Account<List<LedgerEntry>>(list);
                var destination = new Account<List<LedgerEntry>>(new List<LedgerEntry>());
                var context = new MoneyTransfer<Account<List<LedgerEntry>>, Account<List<LedgerEntry>>>(source, destination, 245m);
                context.Transfer();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
        }
    }
}
