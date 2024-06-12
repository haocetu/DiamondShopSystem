using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderDTOs
{
    public class CreateOrderDTO
    {
        public int accountID {  get; set; }
        public int PaymentID { get; set; }
        public decimal totalPrice {  get; set; }
        public string status {  get; set; }
    }
}
