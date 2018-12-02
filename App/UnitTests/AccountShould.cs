using Bank_Kata;
using Bank_Kata.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using TestUtilities.MockBuilders;
using Xunit;

namespace UnitTests
{
    public class AccountShould
    {
        [Fact]
        public void Deposit_100_AmountDeposited()
        {
            var transactions = new TransactionsMockBuilder().Build();

            new Account(transactions).Deposit(100, "01/04/2014");

            Mock.Get(transactions)
                .Verify(t => t.Add("01/04/2014|100.00|100.00"));
        }

        [Fact]
        public void Deposit100_Deposit50_AmountsDeposited()
        {
            var transactions = new TransactionsMockBuilder().Build();
            var account = new Account(transactions);

            account.Deposit(100, "01/04/2014");
            account.Deposit(50, "01/04/2014");

            Mock.Get(transactions)
                .Verify(t => t.Add("01/04/2014|100.00|100.00"));
            Mock.Get(transactions)
                .Verify(t => t.Add("01/04/2014|50.00|150.00"));
        }

        [Fact]
        public void Withdraw100_EmptyAccount_ThrowsException()
        {
            Action withdraw = () => new Account
                (
                    new TransactionsMockBuilder().Build()
                )
                .Withdraw(100, "02/04/2014");

            withdraw.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Deposit250_Withdraw100_TransactionsAdded()
        {
            var transactions = new TransactionsMockBuilder().Build();
            var account = new Account(transactions);

            account.Deposit(250, "10/10/2018");
            account.Withdraw(100, "11/10/2018");

            Mock.Get(transactions)
                .Verify(t => t.Add("10/10/2018|250.00|250.00"));
            Mock.Get(transactions)
                .Verify(t => t.Add("11/10/2018|-100.00|150.00"));
        }

        [Fact]
        public void Deposit100_Withdraw200_ThrowsException()
        {
            var account = new Account
            (
                new TransactionsMockBuilder().Build()
            );

            account.Deposit(100, "10/10/2018");
            Action withdraw = () => account.Withdraw(200, "10/10/2018");

            withdraw.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void PrintStatement_EmptyAccount_PrintsEmptyStatement()
        {
            var console = new ConsoleMockBuilder().Build();

            new Account
                (
                    new List<string>()
                )
                .PrintStatement(console);

            Mock.Get(console)
                .Verify(m => m.WriteLine("DATE|AMOUNT|BALANCE"));
            Mock.Get(console)
                .Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Deposit100_PrintStatement_PrintsTransactions()
        {
            var console = new ConsoleMockBuilder().Build();
            var account = new Account(new List<string>());

            account.Deposit(100, "10/10/2018");
            account.PrintStatement(console);

            Mock.Get(console)
                .Verify(m => m.WriteLine("DATE|AMOUNT|BALANCE"));
            Mock.Get(console)
                .Verify(m => m.WriteLine("10/10/2018|100.00|100.00"));
            Mock.Get(console)
                .Verify(m => m.WriteLine(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void Deposit100_Withdraw50_PrintStatement_PrintsTransactions()
        {
            var console = new ConsoleMockBuilder().Build();
            var account = new Account(new List<string>());

            account.Deposit(100, "10/10/2018");
            account.Withdraw(50, "10/10/2018");
            account.PrintStatement(console);

            Mock.Get(console)
                .Verify(m => m.WriteLine("DATE|AMOUNT|BALANCE"));
            Mock.Get(console)
                .Verify(m => m.WriteLine("10/10/2018|100.00|100.00"));
            Mock.Get(console)
                .Verify(m => m.WriteLine("10/10/2018|-50.00|50.00"));
            Mock.Get(console)
                .Verify(m => m.WriteLine(It.IsAny<string>()), Times.Exactly(3));
        }
    }
}