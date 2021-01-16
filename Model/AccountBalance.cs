namespace CryptoPortfolioReader.Model
{
     public class AccountBalance
    {
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal Available { get; set; }
        public decimal Hold { get; set; }

        public override string ToString()
        {
            return $"{Currency} {Balance:0.000000}";
        }
    }
}