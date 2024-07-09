using Domain.Enums;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public int Point { get; set; }
        public string? ConfirmationToken { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
