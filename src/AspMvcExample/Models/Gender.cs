using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspMvcExample.Models
{
    public class Gender
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public ICollection<Client> Clients { get; set; }


        public Gender()
        {
        }

        public Gender(string name)
        {
            Name = name;
        }
    }
}
