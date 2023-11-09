using Dapper;
using System.Data.SqlClient;
using System.Data;
using CasinoAPI.Models.Dto;

namespace CasinoAPI.Data
{
    public class DataStudio
    {
        Connection cn = new Connection();

        public async Task<List<User>> ShowUsers() {

            using (var connection = new SqlConnection(cn.getConnection()))
            {
                await connection.OpenAsync();
                 var users = connection.Query<User>("ShowUsers",
                      commandType: CommandType.StoredProcedure).ToList();
                
                return users;
            }
        }

        public List<User> ShowUserById(int id)
        {
            using (var connection = new SqlConnection(cn.getConnection()))
            {
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", id);

                var user = connection.Query<User>("ShowUserById", parameters,
                     commandType: CommandType.StoredProcedure).ToList();

                return user;
            }
        }

        public async Task EditUser(User user)
        {
            using (var connection = new SqlConnection(cn.getConnection()))
            {
                await connection.OpenAsync();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", user.id);
                parameters.Add("name", user.name);

                connection.Query("UpdateUser", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task CreateUser(User user)
        {
            using(var connection = new SqlConnection(cn.getConnection()))
            {
                await connection.OpenAsync();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("name", user.name);

                connection.Query("CreateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteUser(int id)
        {
            using(var connection =new SqlConnection(cn.getConnection()))
            {
                await connection.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("id", id);

                connection.Query("DeleteUser", parameters, 
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
