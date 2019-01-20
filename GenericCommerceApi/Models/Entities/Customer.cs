using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi.Models.Entities
{
    public class Customer : BaseEntity
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}
