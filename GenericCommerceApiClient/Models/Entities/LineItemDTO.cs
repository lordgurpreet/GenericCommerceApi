﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApiClient.Models.Entities
{
    public class LineItemDTO : BaseEntity
    {
        public int ProductId { get; set; }
        public int LineItemQuantity { get; set; }
    }
}
