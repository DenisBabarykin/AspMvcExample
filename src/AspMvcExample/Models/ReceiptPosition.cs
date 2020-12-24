using System;
using System.Collections.Generic;
using System.Text;

namespace AspMvcExample.Models
{
    public class ReceiptPosition
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ReceiptId { get; set; }

        public int Amount { get; set; }


        public Product Product { get; set; }

        public Receipt Receipt { get; set; }


        public ReceiptPosition()
        {
        }

        public ReceiptPosition(int productId, int receiptId, int amount)
        {
            ProductId = productId;
            ReceiptId = receiptId;
            Amount = amount;
        }
    }
}
