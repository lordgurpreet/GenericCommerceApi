using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi.Models.Entities
{
    public class OrderDTO : BaseEntity
    {
        public int OrderCustomerId { get; set; }
        public List<LineItemDTO> OrderLineItems { get; set; }
    }
}
