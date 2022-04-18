using SimpleTrading.Deposit.Postgresql.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleTrading.Payments.Abstractions;

namespace SimpleTrading.Deposit.Postgresql.Repositories
{
    public interface IDepositRepository
    {
        Task Add(DepositModel item);
        Task Update(DepositModel item);
        Task<DepositModel> FindById(string id);
        Task<IEnumerable<DepositModel>> FindByTraderId(string traderId);
        Task<IEnumerable<DepositModel>> GetBetweenDates(DateTime from, DateTime to);
        Task<IEnumerable<DepositModel>> FindByPsId(string id);
        Task<IEnumerable<DepositModel>> GetAll();
        Task UpdateOnCallback(string id, PaymentInvoiceStatusEnum status, string commission);
        Task UpdatePsTransactionIdByTransactionId(string transactionId, string psTransactionId);
    }
}