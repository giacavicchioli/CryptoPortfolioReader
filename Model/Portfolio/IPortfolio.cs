using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoPortfolioReader.Model.Portfolio
{
    public interface IPortfolio
    {
        string Name { get; }

       Task<IEnumerable<AccountBalance>> ReadAccountBalances();
    }
}