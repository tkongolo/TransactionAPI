namespace TransactionAPI.Models
{
    public class UserResetToken
    {
        public int Id { get; set; }
        public Users? User { get; set; }
        public string? ResetToken { get; set; }
    }
}
