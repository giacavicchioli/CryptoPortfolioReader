using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CryptoPortfolioReader.Model.Portfolio;
using CryptoPortfolioReader.Services;
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

        internal TelegramConfiguration TelegramConfiguration { get; private set; }

        internal void ReadFromFile(string filepath)
        {
            var config = new Configuration();

            // Telegram
            if (tryCreateTelegram(filepath, out TelegramConfiguration telegramConfig))
            {
                this.TelegramConfiguration = telegramConfig;
            }

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

        private bool tryCreateTelegram(string filepath, out TelegramConfiguration telegramConfig)
        {
            try
            {
                JObject obj = JObject.Parse(File.ReadAllText(filepath));
                telegramConfig = JsonConvert.DeserializeObject<TelegramConfiguration>(obj["telegram"].ToString());
                return true;
            }
            catch
            {
                telegramConfig = null;
                return false;
            }
        }

        private bool tryCreateBinance(string filepath, out BinancePortfolio binancePortfolio)
        {
            try
            {
                var config = getPortfolioCofigurationFromFile<BinanceConfiguration>(filepath, "Binance");
                binancePortfolio = new BinancePortfolio(config);
                return true;
            }
            catch
            {
                binancePortfolio = null;
                return false;
            }
        }

        private bool tryCreateCoinbasePro(string filepath, out CoinbaseProPortfolio coinbaseProPortfolio)
        {
            try
            {
                CoinbaseProConfiguration config = getPortfolioCofigurationFromFile<CoinbaseProConfiguration>(filepath, "CoinbasePro");
                coinbaseProPortfolio = new CoinbaseProPortfolio(config);
                return true;
            }
            catch
            {
                coinbaseProPortfolio = null;
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