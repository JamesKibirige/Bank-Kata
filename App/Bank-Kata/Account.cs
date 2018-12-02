using Bank_Kata.Interfaces;
using System;
using System.Collections.Generic;

namespace Bank_Kata
{
    public class Account
    {
        private readonly IList<string> _transactions;
        private int _balance;

        public Account(IList<string> transactions)
        {
            _transactions = transactions;
        }

        public void Deposit(int amount, string date)
        {
            _balance += amount;
            _transactions.Add
            (
                $"{date}|{amount:F2}|{_balance:F2}"
            );
        }

        public void Withdraw(int amount, string date)
        {
            if (_balance - amount < 0)
            {
                throw new InvalidOperationException($"Insufficient funds to withdraw {amount:F2}, Current Balance {_balance:F2}");
            }

            _balance -= amount;
            _transactions.Add
            (
                $"{date}|-{amount:F2}|{_balance:F2}"
            );
        }

        public void PrintStatement(IConsoleAdapter console)
        {
            console.WriteLine("DATE|AMOUNT|BALANCE");
            foreach (var transaction in _transactions)
            {
                console.WriteLine(transaction);
            }
        }
    }
}