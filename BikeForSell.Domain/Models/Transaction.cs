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
        public int BuyerId { get; set; }
        public int SalemanId { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }

        // 1:1 Bike To Transaction
        public int BikeRef { get; set; }
        public Bike Bike { get; set; }
    }
}
