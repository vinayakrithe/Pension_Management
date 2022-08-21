using AuthenticationMicroservice;
using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Repository;
using AuthenticationMicroservice.Repository.IRepository;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationMicroservice.Test
{
    public class UserRepositoryTest:IClassFixture<ApplicationDbFixture>
    {
        private readonly UserRepository _userRepository;
        private readonly AppSettings _appSettings;
        public UserRepositoryTest(ApplicationDbFixture applicationDbFixture)
        {
            _userRepository = new UserRepository(applicationDbFixture._db, applicationDbFixture._config);
            _appSettings = applicationDbFixture._config.Value;
        }

        [Theory]
        [InlineData("vinayak", "Vinayak123@")]
        public async Task Autenticate_RetursSuccess(string username, string password)
        {
            User user = await _userRepository.Authenticate(username, password);
            Assert.Equal(username, user.Username);
        }

        [Theory]
        [InlineData(null, null)]
        public async Task Autenticate_RetursSo(string username, string password)
        {
            User user = await _userRepository.Authenticate(username, password);
            User exuser = null;
            Assert.Equal(exuser, user);
        }
    }
}
