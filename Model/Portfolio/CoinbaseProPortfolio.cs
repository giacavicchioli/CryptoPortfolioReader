using System.Linq;
using System.Collections.Generic;
using Coinbase.Pro;
using System.Threading.Tasks;
using System;

namespace CryptoPortfolioReader.Model.Portfolio
{
    public class CoinbaseProPortfolio : IPortfolio
    {
        public string Name => "Coinbase Pro";

        private readonly CoinbaseProConfiguration config;

        public CoinbaseProPortfolio(CoinbaseProConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEnumerable<AccountBalance>> ReadAccountBalances()
        {
            var result = new List<AccountBalance>();
            try
            {
                using (var client = new CoinbaseProClient(config.GetConfig()))
                {
                    var getAllAccounts = await client.Accounts.GetAllAccountsAsync();

                    foreach (var a in getAllAccounts)
                    {
                        result.Add(new AccountBalance()
                        {
                            Currency = a.Currency,
                            Balance = a.Balance,
                            Available = a.Available,
                            Hold = a.Hold
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                // logger.Error(exc, exc.Message);
                Console.WriteLine(exc);
            }

            return result;
        }
    }
}