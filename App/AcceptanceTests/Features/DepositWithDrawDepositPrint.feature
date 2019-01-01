Feature: DepositWithDrawDepositPrint
	Bank Kata Acceptance criteria

Scenario: Deposit 1000 Withdraw 100  Deposit 500 Print
	Given a client makes a deposit of '1000' on '01/04/2014'
	And a withdrawal of '100' on '02/04/2014'
	And a client makes a deposit of '500' on '10/04/2014'
	When she prints her bank statement 
	Then she would see: 'DATE|AMOUNT|BALANCE\r\n10/04/2014|500.00|1400.00\r\n02/04/2014|-100.00|900.00\r\n01/04/2014|1000.00|1000.00'
