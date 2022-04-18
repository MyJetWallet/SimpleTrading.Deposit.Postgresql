using SimpleTrading.Deposit.Postgresql.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTrading.Deposit.Postgresql.Repositories
{
    public interface IWalletRepository
    {
        Task Add(WalletModel model);
        Task<IEnumerable<WalletModel>> GetByTraderId(string traderId);
        Task<WalletModel> GetById(string id);
        Task<WalletModel> GetByAccountId(string accountId);
        Task<IEnumerable<WalletModel>> GetAll();
        Task<WalletModel> FindByTraderIdAccountIdAndCurrency(string traderId,
            string accountId, string currency);
    }
}