using System.Collections.Generic;

namespace DogGoMVC.Models.ViewModels
{
    public class WalkFormViewModel
    {
        public Walks Walks { get; set; }
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }

        public List<int> SelectedDogs { get; set; } = new List<int>();

/*        public WalkerProfileViewModel WalkerProfile     { get; set; }
*/
    }
}