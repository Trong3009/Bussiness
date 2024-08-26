using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;

namespace Web.BLL
{
    public interface IUserService
    {
        Task<BaseViewPage> SetUserAsync(Users user);
        Task<List<Users>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<Users> GetByIdAsync(Guid userId);
        Task<BaseViewPage> DaleteUserAsync(Guid userid);
    }
}
