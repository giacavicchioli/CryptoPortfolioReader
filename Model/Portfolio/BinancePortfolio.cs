using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;

namespace CryptoPortfolioReader.Model.Portfolio
{
    public class BinancePortfolio : IPortfolio
    {
        private readonly BinanceConfiguration config;
        public BinancePortfolio(BinanceConfiguration config)
        {
            this.config = config;
        }

        public string Name => throw new System.NotImplementedException();

        public async Task<IEnumerable<AccountBalance>> ReadAccountBalances()
        {
            var result = new List<AccountBalance>();

            var clientConfig = new BinanceClientOptions
            {
                ApiCredentials = new ApiCredentials(config.ApiKey, config.ApiSecret)
            };

            using (var client = new BinanceClient(clientConfig))
            {
                var res = await client.General.GetAccountInfoAsync();
                foreach (var balance in res.Data.Balances)
                {
                    result.Add(new AccountBalance()
                    {
                        Hold = balance.Locked,
                        Available = balance.Free,
                        Balance = balance.Total,
                        Currency = balance.Asset
                    });
                }
            }

            return result;
        }
    }
}