using System.Collections.Generic;
using System.Threading.Tasks;
using epita_ca1_74526.Controllers;
using epita_ca1_74526.Data.Enum;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Tests.Controller
{
    [TestFixture]
    public class TransactionControllerTests
    {
        [Test]
        public async Task Transaction_Controller_Index_ReturnsSuccess()
        {
            // Arrange
            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction {  Id = 1,
                Date = DateTime.Now,
                Amount = 100,
                Balance = 500,
                AccountId = 1,
                UserId = 1,
                Title = "Fake Transaction 1",
                transactionType = TransactionType.Transferred  },
                new Transaction {  Id = 1,
                Date = DateTime.Now,
                Amount = 100,
                Balance = 500,
                AccountId = 1,
                UserId = 1,
                Title = "Fake Transaction 2",
                transactionType = TransactionType.Withdraw  },
            };
            mockTransactionRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedTransactions);
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockAccountBankRepository = new Mock<IAccountBankRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var controller = new TransactionController(
                mockTransactionRepository.Object,
                mockHttpContextAccessor.Object,
                mockAccountBankRepository.Object,
                mockUserRepository.Object);

            var result = await controller.Index();

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<Transaction>;
            Assert.IsNotNull(model);
            Assert.That(model.Count(), Is.EqualTo(expectedTransactions.Count));
        }


        [Test]
        public async Task Transaction_Controller_Detail_ReturnsSuccess()
        {
            int transactionId = 1;
            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var mockAccountBankRepository = new Mock<IAccountBankRepository>();

            var fakeTransaction = new Transaction { Id = transactionId, AccountId = 1 };
            mockTransactionRepository.Setup(repo => repo.GetByIdAsync(transactionId)).ReturnsAsync(fakeTransaction);

            var fakeAccountBank = new AccountBank { Id = 1, UserId = 1, Name = "Test Account", accountType = AccountType.Saving };
            mockAccountBankRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeAccountBank);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserRepository = new Mock<IUserRepository>();

            var controller = new TransactionController(
                mockTransactionRepository.Object,
                mockHttpContextAccessor.Object,
                mockAccountBankRepository.Object,
                mockUserRepository.Object);

            var result = await controller.Detail(transactionId);

            Assert.IsTrue(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model is DetailTransactionViewModel, Is.True);
            var viewModel = viewResult.Model as DetailTransactionViewModel;
            Assert.Multiple(() =>
            {
                Assert.That(viewModel.transaction, Is.EqualTo(fakeTransaction));
                Assert.That(viewModel.accountBank, Is.EqualTo(fakeAccountBank));
            });
        }

        [Test]
        public async Task Transaction_Controller_Detail_RedirectToError()
        {
            int transactionId = 1;
            var mockTransactionRepository = new Mock<ITransactionRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockAccountBankRepository = new Mock<IAccountBankRepository>();

            var controller = new TransactionController(
               mockTransactionRepository.Object,
               mockHttpContextAccessor.Object,
               mockAccountBankRepository.Object,
               mockUserRepository.Object);

            var result = await controller.Detail(transactionId);

            Assert.IsTrue(result is RedirectToActionResult);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Error"));
                Assert.That(redirectToActionResult.ControllerName, Is.EqualTo("Home"));
            });
        }
    }
}