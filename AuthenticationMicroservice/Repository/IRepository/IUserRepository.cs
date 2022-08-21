using AuthenticationMicroservice.Models;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Repository.IRepository
{
    public interface IUserRepository
    {
       // bool IsUniqueUser(string username);
        //bool IsUniqueEmail(string email);
        Task<User> Authenticate(string username, string password);
        //Task<User> Register(string email,string username, string password);
    }
}
