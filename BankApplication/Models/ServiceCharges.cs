namespace BankApplication.Models
{
    struct ServiceCharges
    {
        public int RTGS;
        public int IMPS;
        public void AssignCharges(int rtgs, int imps)
        {
            RTGS = rtgs;
            IMPS = imps;
        }
        public float getValue(float amount)
        {
            if (amount < 200000)
            {
                return amount * IMPS / 100;
            }
            else
            {
                return amount * RTGS / 100;
            }
        }
    }
}
