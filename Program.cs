using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoPortfolioReader.Model;
using CryptoPortfolioReader.Services;

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

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current assets");
            foreach (var a in summer.AccountBalances.Where(c => c.Balance > 0).OrderBy(c => c.Currency))
            {
                sb.AppendLine(a.ToString());
            }

            var telegram = new TelegramHandler(Configuration.Instance.TelegramConfiguration);
            await telegram.SendMessage(sb.ToString());
            Console.WriteLine(sb.ToString());
            Console.WriteLine("Done");
        }
    }
}
