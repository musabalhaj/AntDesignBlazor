using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class PurchaseDetails
    {
        public int Id { get; set; }

        public int PurchasesId { get; set; }
        public Purchases Purchases { get; set; }
        public string Item { get; set; }

        public string Price { get; set; }
        public string Quantaty { get; set; }

    }
}
