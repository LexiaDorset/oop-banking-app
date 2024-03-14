using epita_ca1_74526.Data.Enum;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;

// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.ViewModels
{
    public class CreateAdminTransactionViewModel
    {

        public string Title { get; set; }
        public int Amount { get; set; }
        public TransactionType transactionType { get; set; }

        public int AccountId { get; set; }
        public int UserId { get; set; }
        [BindNever]
        public IEnumerable<AccountBank>? UserAccounts { get; set; }

    }
}
