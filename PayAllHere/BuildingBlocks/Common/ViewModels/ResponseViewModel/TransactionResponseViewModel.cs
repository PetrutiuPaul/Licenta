namespace Common.ViewModels.ResponseViewModel
{
    public class TransactionResponseViewModel
    {
        public string Id { get; set; }

        public double Value { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public bool Validated { get; set; }
    }

}
