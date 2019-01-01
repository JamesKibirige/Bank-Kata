using Bank_Kata;
using System.Collections.Generic;
using Bank_Kata.Interfaces;
using Moq;
using TechTalk.SpecFlow;
using TestUtilities.MockBuilders;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class DepositWithDrawDepositPrintSteps
    {
        private readonly IConsoleAdapter _consoleAdapter;
        private readonly Account _account;

        public DepositWithDrawDepositPrintSteps()
        {
            _consoleAdapter = new ConsoleMockBuilder().Build();
            _account = new Account(new List<string>());
        }

        [Given(@"a client makes a deposit of '(.*)' on '(.*)'")]
        public void GivenAClientMakesADepositOfOn(int amount, string date)
        {
            _account.Deposit(amount, date);
        }

        [Given(@"a withdrawal of '(.*)' on '(.*)'")]
        public void GivenAWithdrawalOfOn(int amount, string date)
        {
            _account.Withdraw(amount, date);
        }

        [When(@"she prints her bank statement")]
        public void WhenShePrintsHerBankStatement()
        {
            _account.PrintStatement(_consoleAdapter);
        }

        [Then(@"she would see: '(.*)'")]
        public void ThenSheWouldSee(string statement)
        {
            var statementParts = statement.Split(@"\r\n");
            foreach (var item in statementParts)
            {
                Mock.Get(_consoleAdapter)
                    .Verify(m => m.WriteLine(item));
            }
        }
    }
}