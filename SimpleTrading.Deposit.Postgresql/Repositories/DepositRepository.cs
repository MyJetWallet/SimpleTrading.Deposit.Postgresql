using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPostgreSQL;
using SimpleTrading.Deposit.Postgresql.Models;
using SimpleTrading.Payments.Abstractions;

namespace SimpleTrading.Deposit.Postgresql.Repositories
{
    public class DepositRepository: IDepositRepository
    {
        private readonly string _dbWriteConnectionString;
        private readonly string _dbReadConnectionString;
        private readonly string _appName;

        public DepositRepository(string writeConnectionString, string readConnectionString, string appName)
        {
            _dbWriteConnectionString = writeConnectionString;
            _dbReadConnectionString = readConnectionString;
            _appName = appName;
        }

        public async Task Add(DepositModel item)
        {
            var db = new PostgresConnection(_dbWriteConnectionString, _appName);
            var sql = $"INSERT INTO deposits (id, paymentsystem, psaggregator, srccurrency, srcamount, amount," +
                          " traderid, accountid, datetime, accepted, comment, status, pstransactionid, currency, extraprofit," +
                          " platformdatetime, brand) VALUES(@TransactionId,@PaymentSystem,@PaymentProvider,@PsCurrency, @PsAmount, " +
                          " @Amount, @TraderId, @AccountId, @DateTime,  null, @Comment, @Status, @PsTransactionId, @Currency, " +
                          "0, @PlatformDateTime, @Brand)";

            await db.ExecAsync(sql, item);
        }

        public async Task Update(DepositModel item)
        {
            var db = new PostgresConnection(_dbWriteConnectionString, _appName);
            var sql = $"UPDATE deposits SET paymentsystem = @PaymentSystem," +
                          $" psaggregator = @PaymentProvider, srccurrency = @PsCurrency, srcamount = @PsAmount," +
                          $" amount = @Amount, traderid = @TraderId, accountid = @AccountId," +
                          $" datetime = @DateTime, comment  = @Comment, status= @Status," +
                          $" pstransactionid= @PsTransactionId, currency= @Currency," +
                          $" platformdatetime= @PlatformDateTime WHERE id = @Id";

            await db.ExecAsync(sql, item);
        }

        public async Task<DepositModel> FindById(string id)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM pci_dss_deposits_view WHERE id = @Id";
            return await db.GetFirstRecordOrNullAsync<DepositModel>(sql, new {Id = id});
        }
        
        public async Task<IEnumerable<DepositModel>> FindByTraderId(string traderId)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM pci_dss_deposits_view WHERE traderid = @Id";
            return await db.GetRecordsAsync<DepositModel>(sql, new {Id = traderId});
        }

        public async Task<IEnumerable<DepositModel>> GetBetweenDates(DateTime from, DateTime to)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM pci_dss_deposits_view where datetime BETWEEN @FromDate and @ToDate";
            return await db.GetRecordsAsync<DepositModel>(sql, new {FromDate = from, ToDate = to});
        }

        public async Task<IEnumerable<DepositModel>> FindByPsId(string id)
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);

            const string sql = "SELECT * FROM pci_dss_deposits_view WHERE pstransactionid = @Id";
            return await db.GetRecordsAsync<DepositModel>(sql, new {Id = id});
        }

        public async Task<IEnumerable<DepositModel>> GetAll()
        {
            var db = new PostgresConnection(_dbReadConnectionString, _appName);
            const string sql = "SELECT * FROM pci_dss_deposits_view";
            return await db.GetRecordsAsync<DepositModel>(sql);
        }

        public async Task UpdateOnCallback(string id, PaymentInvoiceStatusEnum status, string commission)
        {
            var db = new PostgresConnection(_dbWriteConnectionString, _appName);
            const string sql =
                "UPDATE deposits SET status = @Status, commission = CAST(@Commission AS json) where id = @Id";
            await db.ExecAsync(sql, new {Status = status, Commission = commission, Id = id});
        }

        public async Task UpdatePsTransactionIdByTransactionId(string transactionId, string psTransactionId)
        {
            var db = new PostgresConnection(_dbWriteConnectionString, _appName);
            const string sql =
                "UPDATE deposits SET pstransactionid = @PsId where id = @Id";
            await db.ExecAsync(sql, new {Id = transactionId, PsId = psTransactionId});
        }
    }
}