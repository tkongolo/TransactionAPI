namespace TransactionAPI.Models
{
    public class UserLoginToken
    {
        public int Id { get; set; }
        public Users? User { get; set; }
        public string? AccessToken { get; set; }
    }
}
