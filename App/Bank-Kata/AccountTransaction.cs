namespace Bank_Kata
{
    public class AccountTransaction
    {
        private readonly int _amount;
        private readonly string _date;

        public AccountTransaction(int amount, string date)
        {
            _amount = amount;
            _date = date;
        }
    }
}