using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CryptoPortfolioReader.Model.Portfolio;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoPortfolioReader.Model
{
    public class Configuration
    {
        private static Configuration instance;
        internal static Configuration Instance
        {
            get
            {
                if (instance == null)
                    instance = new Configuration();
                return instance;
            }
        }

        private Configuration() { }

        internal List<IPortfolio> Portfolios { get; } = new List<IPortfolio>();

        internal void ReadFromFile(string filepath)
        {
            var config = new Configuration();

            // Coinbase Pro
            if (tryCreateCoinbasePro(filepath, out CoinbaseProPortfolio coinbaseProPortfolio))
            {
                this.Portfolios.Add(coinbaseProPortfolio);
            }

            // Binance
            if (tryCreateBinance(filepath, out BinancePortfolio binancePortfolio))
            {
                this.Portfolios.Add(binancePortfolio);
            }
        }

        private bool tryCreateBinance(string filepath, out BinancePortfolio binancePortfolio)
        {
            binancePortfolio = null;
            try
            {
                var config = getPortfolioCofigurationFromFile<BinanceConfiguration>(filepath, "Binance");
                binancePortfolio = new BinancePortfolio(config);
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        private bool tryCreateCoinbasePro(string filepath, out CoinbaseProPortfolio coinbaseProPortfolio)
        {
            coinbaseProPortfolio = null;

            try
            {
                CoinbaseProConfiguration config = getPortfolioCofigurationFromFile<CoinbaseProConfiguration>(filepath, "CoinbasePro");
                coinbaseProPortfolio = new CoinbaseProPortfolio(config);
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        private T getPortfolioCofigurationFromFile<T>(string filepath, string key)
        {
            JObject obj = JObject.Parse(File.ReadAllText(filepath));

            var configJson = ((JArray)obj["portfolios"]).FirstOrDefault(c => c.Value<string>("name") == key)?.SelectToken("config");

            if (configJson == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(configJson.ToString());
        }

    }
}