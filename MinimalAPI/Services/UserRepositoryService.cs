using MinimalAPI.DTO;
using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface IUserRepositoryService
    {
        UserDto GetUser(UserModel userModel);
    }
    public class UserRepositoryService : IUserRepositoryService
    {
        private List<UserDto> _users => new()
        {
            new("admin", "abc123")
        };

        public UserDto GetUser(UserModel userModel) =>
             _users.FirstOrDefault(x => string.Equals(x.UserName, userModel.UserName) && string.Equals(x.Password, userModel.Password));
        
    }
}
