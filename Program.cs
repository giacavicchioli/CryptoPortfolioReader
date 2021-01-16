using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoPortfolioReader.Model;

namespace CryptoPortfolioReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Configuration.Instance.ReadFromFile("./config.json");

            var taskPool = new List<Task<IEnumerable<AccountBalance>>>();
            foreach (var portfolio in Configuration.Instance.Portfolios)
            {
                taskPool.Add(portfolio.ReadAccountBalances());
            }

            var res = await Task.WhenAll(taskPool);

            var summer = new AccountBalancesSummer();
            foreach (var ab in res)
            {
                summer.Add(ab);
            }

            foreach (var a in summer.AccountBalances.Where(c => c.Balance > 0).OrderBy(c => c.Currency))
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("Done");
        }
    }
}
