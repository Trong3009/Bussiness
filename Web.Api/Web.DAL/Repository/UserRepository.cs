using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;

namespace Web.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WebConnection");
        }

        public async Task<BaseViewPage> DeleteUser(Guid userid)
        {
            const string storedProcedure = "UM_Users_Del";
            using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("@userId", userid);
                    return await connection.QueryFirstAsync<BaseViewPage>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                }
        }

        public async Task<List<Users>> GetAll(int pageNumber, int pageSize)
        {
            const string storedProcedure = "UM_Users_GetPages";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("@PageNumber", pageNumber);
                    param.Add("@PageSize", pageSize);
                    param.Add("@Name");
                    var result = await connection.QueryAsync<Users>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> GetById(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string storedProcedure = "UM_Users_GetDetails";
                connection.Open();
                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                var rs = await connection.QuerySingleOrDefaultAsync<Users>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                return rs;
            }
        }

        public async Task<BaseViewPage> SetUser(Users user)
        {
            const string storedProcedure = "UM_Users_Set";
            try
            {
                using(var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("@UserId", user.UserId);
                    param.Add("@UserName", user.UserName);
                    param.Add("@Name", user.FullName);
                    param.Add("@Email", user.Email);
                    param.Add("@Phone", user.Phone);
                    param.Add("@Password", user.Password);
                    param.Add("@Avatar", user.Avatar ?? (object)DBNull.Value, DbType.Binary);
                    param.Add("@Status", user.Status);
                    var result = await connection.QueryFirstOrDefaultAsync<BaseViewPage>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
