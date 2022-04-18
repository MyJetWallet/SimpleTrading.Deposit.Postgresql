using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPostgreSQL;
using SimpleTrading.Deposit.Postgresql.Models;

namespace SimpleTrading.Deposit.Postgresql.Repositories
{
    public class WalletRepository: IWalletRepository
    {
        private readonly string _dbWriteConnectionString;
        private readonly string _dbReadConnectionString;
        private readonly string _appName;

        public WalletRepository(string writeConnectionString, string readConnectionString, string appName)
        {
            _dbWriteConnectionString = writeConnectionString;
            _dbReadConnectionString = readConnectionString;
            _appName = appName;
        }

        public async Task Add(WalletModel model)
        {
            var db = new PostgresConnection(_dbWriteConnectionString, _appName);
            const string sql =
                    "INSERT into wallets (id, traderid, currencyid, accountid) VALUES (@Id, @TraderId, @CurrencyId, @AccountId)";
            await db.ExecAsync(sql, model);
        }

        public async Task<IEnumerable<WalletModel>> GetByTraderId(string traderId)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM wallets where traderid = @TraderId";
            return await db.GetRecordsAsync<WalletModel>(sql, new { TraderId = traderId });
        }

        public async Task<WalletModel> GetById(string id)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM wallets where id = @Id";
            return await db.GetFirstRecordOrNullAsync<WalletModel>(sql,
                new { Id = id });
        }

        public async Task<WalletModel> GetByAccountId(string accountId)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM wallets where accountid = @Id";
            return await db.GetFirstRecordOrNullAsync<WalletModel>(sql,
                new { Id = accountId });
        }

        public async Task<IEnumerable<WalletModel>> GetAll()
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM wallets";
            return await db.GetRecordsAsync<WalletModel>(sql);
        }

        public async Task<WalletModel> FindByTraderIdAccountIdAndCurrency(string traderId,
            string accountId, string currency)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);            
            const string sql =
                "SELECT * FROM wallets where traderid = @TraderId and accountid = @AccountId and currencyid = @Currency";
            return await db.GetFirstRecordOrNullAsync<WalletModel>(sql,
                new
                {
                    TraderId = traderId,
                    AccountId = accountId,
                    Currency = currency
                });
        }
    }
}