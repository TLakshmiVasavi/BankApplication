namespace BankApplication.Models
{
    struct Currency
    {
        public string Name;
        public float ExchangeRate;

        public Currency(string name, float rate)
        {
            Name = name;
            ExchangeRate = rate;
        }
    }
}
