using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpireYearMonth { get; set; }
        public string Cvv { get; set; }
        public string CardHolder { get; set; }
        public decimal Balance { get; set; }
    }
}
