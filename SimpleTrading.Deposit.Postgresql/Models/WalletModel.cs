namespace SimpleTrading.Deposit.Postgresql.Models
{
    public class WalletModel
    {
        public string Id { get; set; }
        public string TraderId { get; set; }
        public string CurrencyId { get; set; }
        public string AccountId { get; set; }
    }
}