using System.Linq;
using System.Collections.Generic;

namespace CryptoPortfolioReader.Model
{
    public class AccountBalancesSummer
    {
        internal List<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();

        internal void Add(IEnumerable<AccountBalance> accountBalanceList)
        {
            foreach (var ab in accountBalanceList)
            {
                var c = this.AccountBalances.FirstOrDefault(k => k.Currency == ab.Currency);

                if (c == null)
                {
                    this.AccountBalances.Add(ab);
                }
                else
                {
                    c.Balance += ab.Balance;
                    c.Available += ab.Available;
                    c.Hold += ab.Hold;
                }
            }
        }

    }
}