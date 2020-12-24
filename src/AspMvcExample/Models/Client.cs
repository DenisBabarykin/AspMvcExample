using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspMvcExample.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public int GenderId { get; set; }


        public Gender Gender { get; set; }

        public ICollection<Receipt> Receipts { get; set; }


        public Client()
        {
        }

        public Client(string name, int genderId, DateTime birthday)
        {
            Name = name;
            GenderId = genderId;
            Birthday = birthday;
        }
    }
}
