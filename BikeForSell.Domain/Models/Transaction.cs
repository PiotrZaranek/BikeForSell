using BikeForSell.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public string SalemanId { get; set; }
        public DateTime Date { get; set; }
        public State State { get; set; }

        // 1:1 Bike To Transaction
        public int BikeRef { get; set; }
        public Bike Bike { get; set; }
    }
}
