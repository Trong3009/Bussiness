using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL;
using Web.Model;

namespace Web.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<BaseViewPage> DaleteUserAsync(Guid userid)
        {
            return _userRepository.DeleteUser(userid);
        }

        public Task<List<Users>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
           return _userRepository.GetAll(pageNumber, pageSize);
        }

        public Task<Users> GetByIdAsync(Guid userId)
        {
            return _userRepository.GetById(userId);
        }

        public Task<BaseViewPage> SetUserAsync(Users user)
        {
            return _userRepository.SetUser(user);
        }
    }
}
