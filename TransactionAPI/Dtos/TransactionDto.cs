namespace TransactionAPI.Dtos
{
    public class TransactionDto
    {
        public string Name { get; set; }
        public float PreviousBalance { get; set; }
        public float NewBalance { get; set; }
    }
}
