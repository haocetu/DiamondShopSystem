namespace Application.ViewModels.OrderDTOs
{
    public class CreateOrderDTO
    {
        public int AccountId { get; set; }
        public int PaymentID { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
