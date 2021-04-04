using System;
using System.Collections.Generic;

namespace OffiRent.API.Domain.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string OwnerName { get; set; }
        public string DueDate { get; set; }
        public string CV { get; set; }
        public List<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();
    }
}
