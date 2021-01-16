using System;
using Coinbase.Pro;

namespace CryptoPortfolioReader.Model.Portfolio
{
    public class CoinbaseProConfiguration
    {
        public string ApiKey;
        public string ApiSecret;
        public string ApiPassphrase;

        internal Config GetConfig()
        {
            return new Config()
            {
                ApiKey = this.ApiKey,
                Secret = this.ApiSecret,
                Passphrase = this.ApiPassphrase
            };
        }
    }
}