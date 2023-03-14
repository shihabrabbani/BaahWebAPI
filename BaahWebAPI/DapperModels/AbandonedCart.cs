namespace BaahWebAPI.DapperModels
{
    public class AbandonedCart
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Customer { get; set; }
        public string OrderTotal { get; set; }
        public string AbandonedDate { get; set; }
        public string Status { get; set; }
    }
}
