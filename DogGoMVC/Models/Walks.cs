using System;

namespace DogGoMVC.Models
{
    public class Walks
    {

        public int Id { get; set; }
        public int Duration { get; set; }
        Walker Walker { get; set; } 
        Dog Dog { get; set; }

        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public DateTime Date { get; set; }

        

    }
}
