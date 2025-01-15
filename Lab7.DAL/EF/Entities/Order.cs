using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DAL.EF.Entities
{
    public class Order
    {
        public Order(int id, int adminId, DateTime createdDate, string? orderDescription, string? status, string? customerFeedback)
        {
            Id = id;
            AdminId = adminId;
            CreatedDate = createdDate;
            OrderDescription = orderDescription;
            Status = status;
            CustomerFeedback = customerFeedback;
        }

        public int Id { get; set; }
        public int AdminId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? OrderDescription { get; set; }
        public string? Status { get; set; }
        public string? CustomerFeedback { get; set; }

        public Admin? Admin { get; set; }
    }
}
