using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Web.Model;

namespace Web.DAL
{
    public interface IUserRepository
    {
        Task<BaseViewPage> SetUser(Users user);
        Task<List<Users>> GetAll(int pageNumber, int pageSize);
        Task<BaseViewPage> DeleteUser(Guid userid);
        Task<Users> GetById(Guid userId);
    }
}
