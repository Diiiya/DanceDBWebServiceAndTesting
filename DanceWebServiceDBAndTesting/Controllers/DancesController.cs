using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DanceDLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DanceWebServiceDBAndTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DancesController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;

        // GET: api/Dances
        [HttpGet]
        public IEnumerable<Dance> Get()
        {
            string selectString = "SELECT * FROM Dance;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Dance> result = new List<Dance>();
                        while (reader.Read())
                        {
                            Dance dance = ReadDances(reader);
                            result.Add(dance);
                        }
                        return result;
                    }
                }
            }
        }

        private Dance ReadDances(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string dname = reader.GetString(1);
            string ddescription = reader.GetString(2);
            string photo = reader.GetString(3);
            string country = reader.GetString(4);
            int timeappeared = reader.GetInt32(5);
            string dtype = reader.GetString(6);
            DateTime addedDate = reader.GetDateTime(7);
            Dance dance = new Dance()
            {
                Id = id,
                DName = dname,
                DDescription = ddescription,
                Photo = photo,
                Country = country,
                TimeAppeared = timeappeared,
                DType = dtype,
                AddedDate = addedDate
            };

            return dance;
        }

        // GET: api/Dances/5
        [HttpGet("{id}", Name = "Get")]
        public Dance Get(int id)
        {
            string selectString = "select * from Dance where id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return ReadDances(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        // POST: api/Dances
        [HttpPost]
        public int Post([FromBody] Dance value)
        {
            string insertString = "insert into dance (dname, ddescription, photo, country, timeappeared, dtype) values(@dname, @ddescription, @photo, @country, @timeappeared, @dtype); ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                    command.Parameters.AddWithValue("@dname", value.DName);
                    command.Parameters.AddWithValue("@ddescription", value.DDescription);
                    command.Parameters.AddWithValue("@photo", value.Photo);
                    command.Parameters.AddWithValue("@country", value.Country);
                    command.Parameters.AddWithValue("@timeappeared", value.TimeAppeared);
                    command.Parameters.AddWithValue("@dtype", value.DType);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Dances/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Dance value)
        {
            const string updateString = "update dance set dname=@dname, ddescription=@ddescription, photo=@photo, country=@country, timeappeared=@timeappeared, dtype=@dtype where id=@id;";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand updateCommand = new SqlCommand(updateString, databaseConnection))
                {
                    updateCommand.Parameters.AddWithValue("@dname", value.DName);
                    updateCommand.Parameters.AddWithValue("@ddescription", value.DDescription);
                    updateCommand.Parameters.AddWithValue("@photo", value.Photo);
                    updateCommand.Parameters.AddWithValue("@country", value.Country);
                    updateCommand.Parameters.AddWithValue("@timeappeared", value.TimeAppeared);
                    updateCommand.Parameters.AddWithValue("@dtype", value.DType);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    return rowsAffected;
                }

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            string deleteString = "delete from dance where id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
