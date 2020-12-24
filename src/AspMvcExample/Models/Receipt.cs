using System;
using System.Collections.Generic;
using System.Text;

namespace AspMvcExample.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateTime Time { get; set; }

        public double Cost { get; set; }


        public Client Client { get; set; }

        public ICollection<ReceiptPosition> ReceiptPositions { get; set; }


        public Receipt()
        {
        }

        public Receipt(int clientId, DateTime time)
        {
            ClientId = clientId;
            Time = time;
        }
    }
}
