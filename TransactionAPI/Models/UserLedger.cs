namespace TransactionAPI.Models
{
    public class UserLedger
    {
        public int Id { get; set; }
        public Users? User { get; set; }
        public string? TransactionType { get; set; }
        public float PreviousBalance { get; set; }
        public float Amount { get; set; }
        public float FinalBalance { get; set; }

    }
}
