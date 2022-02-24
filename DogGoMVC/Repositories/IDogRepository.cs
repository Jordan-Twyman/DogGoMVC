using DogGoMVC.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGoMVC.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();

        Dog GetDogById(int id);

        void AddDog(Dog dog);

        void UpdateDog(Dog dog);

        void DeleteDog(int dogId);
        List<Dog> GetDogsByOwnerId(int ownerId);


    }
}