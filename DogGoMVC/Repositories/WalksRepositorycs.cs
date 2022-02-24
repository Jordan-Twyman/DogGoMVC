using DogGoMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGoMVC.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }



        public List<Walks> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Date, Duration, WalkerId, DogId
                        FROM Walks
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")) 


                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public Walks GetWalksById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Date, Duration, DogId, WalkerId
                        FROM Walks
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walks walks = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))

                        };

                        reader.Close();
                        return walks;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public List<Walks> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Date, Duration, WalkerId, DogId 
                FROM Walks
                WHERE WalkerId = @walkerId
            ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();

                    while (reader.Read())
                    {
                        Walks walk = new Walks()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };

                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }


        public void AddWalks(Walks walks)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walks (Date, Duration, DogId, WalkerId)
                    OUTPUT INSERTED.ID
                    VALUES (@date, @duration, @walkerId, @dogId);
                ";

                    cmd.Parameters.AddWithValue("@date", walks.Date);
                    cmd.Parameters.AddWithValue("@duration", walks.Duration);
                    cmd.Parameters.AddWithValue("@walkerId", walks.WalkerId);
                    cmd.Parameters.AddWithValue("@dogId", walks.DogId);

                    int id = (int)cmd.ExecuteScalar();

                    walks.Id = id;
                }
            }
        }

        public void UpdateWalks(Walks walks)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Walks
                            SET 
                                Date = @date, 
                                Duration = @duration, 
                                WalkerId = @walkerId, 
                                DogId = @dogId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@date", walks.Date);
                    cmd.Parameters.AddWithValue("@duration", walks.Duration);
                    cmd.Parameters.AddWithValue("@dogId", walks.DogId);
                    cmd.Parameters.AddWithValue("@walkerId", walks.WalkerId);
                    cmd.Parameters.AddWithValue("@id", walks.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteWalks(int walksId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Walks
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", walksId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}