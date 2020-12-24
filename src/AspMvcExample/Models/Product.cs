using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspMvcExample.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Cost { get; set; }


        public ICollection<ReceiptPosition> ReceiptPositions { get; set; }


        public Product()
        {
        }

        public Product(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}
