using System;
using System.Collections.Generic;

namespace DogGoMVC.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public DateTime Date { get; set; }

        public Walker Walker { get; set; }

        public List <Neighborhood> Neighborhood { get; set; }

        public Walks Duration { get; set; }

        public List <Walks> Walks { get; set; }

        public List <Owner> Owner { get; set; }

        public List<Dog> Dog { get; set; }



    }

}
