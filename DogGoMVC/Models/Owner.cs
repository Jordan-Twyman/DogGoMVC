using DogGoMVC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogGoMVC.Models
{
    public class Owner
    {
       public List <Dog> Dog { get; set; }


        public int Id { get; set; }

        public int NeighborhoodId { get; set; }

        public string Name { get; set; }
            
        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Neighborhood Neighborhood { get; set; }

    }
}