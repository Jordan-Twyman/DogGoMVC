using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        [DataType(DataType.Date)]   

        public DateTime? Date { get; set; }

        

    }
}
