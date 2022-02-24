using DogGoMVC.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGoMVC.Repositories
{
    public interface IWalksRepository
    {
        List<Walks> GetAllWalks();

        Walks GetWalksById(int id);

        void AddWalks(Walks walks);

        void UpdateWalks(Walks walks);

        void DeleteWalks(int walkid);
        List<Walks> GetWalksByWalkerId(int walkerId);


    }
}