using System;
using SimpleTrading.Payments.Abstractions;

namespace SimpleTrading.Deposit.Postgresql.Models
{
    public class DepositModel
    {
        public string Id { get; set; }
        public string TransactionId => Id;
        public string PaymentSystem { get; set; }
        public string PsTransactionId { get; set; }
        public string PaymentProvider { get; set; }
        public string PsCurrency { get; set; }
        public double PsAmount { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public PaymentInvoiceStatusEnum Status { get; set; }
        public string TraderId { get; set; }
        public string AccountId { get; set; }
        public string AdminId { get; set; }
        public string Comment { get; set; }
        public string VoidTransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime PlatformDateTime { get; set; }
        public BrandName Brand { get; set; }
    }
}