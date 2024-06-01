using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        public string? Name {  get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber {  get; set; }
        public string? Gender {  get; set; }
        public int? RoleId { get; set; }
        public decimal? Point {  get; set; }
        public string? ConfirmationToken { get; set; }
        //Relationship
        public virtual Role? Role { get; set; }
        public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
