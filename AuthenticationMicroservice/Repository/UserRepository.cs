
using AuthenticationMicroservice.Data;
using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Repository.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserRepository));

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                _log4net.Error(" User not found in database : From Authenticate method of: " + nameof(UserRepository));
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            user.ExpirationDate = DateTime.UtcNow.AddHours(1);
            _log4net.Info("JWT Token successfuly generated : From Authenticate method of: " + nameof(UserRepository));
            return user;
        }

        //public bool IsUniqueUser(string username)
        //{
        //    var user = _db.Users.SingleOrDefault(x => x.Username == username);

        //    if (user == null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public bool IsUniqueEmail(string email)
        //{
        //    var user = _db.Users.SingleOrDefault(x => x.Email == email);

        //    if (user == null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<User> Register(string email,string username, string password)
        //{
        //    User userObj = new User()
        //    {
        //        Email = email,
        //        Username = username,
        //        Password = password
        //    };

        //    _db.Users.Add(userObj);
        //    _db.SaveChanges();
        //    userObj.Password = "";
        //    return userObj;
        //}
    }
}
