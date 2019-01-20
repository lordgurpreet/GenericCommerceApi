using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApiClient.Models.Entities
{
    public class Order : BaseEntity
    {
        public bool OrderFulfilled { get; set; }

        [ForeignKey("Customer")]
        public int OrderCustomerId { get; set; }
        public Customer OrderCustomer { get; set; }

        public ICollection<LineItem> OrderLineItems { get; set; }

        public Order()
        {
            if (OrderLineItems == null)
                OrderLineItems = new List<LineItem>();
        }
    }
}
