using AuthenticationMicroservice.Controllers;
using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Repository;
using AuthenticationMicroservice.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationMicroservice.Test
{
    public class UsersControllerTest
    {
        private readonly Mock<IUserRepository> service;

        public UsersControllerTest()
        {
            service = new Mock<IUserRepository>();
        }

        [Fact]
        public async void AuthenticationWorks()
        {
            var user = await GetSampleDetails();
            service.Setup(x => x.Authenticate("vinayak","Vinayak123@")).Returns(GetSampleUserDetails);
            var controller = new UsersController(service.Object);

            //act
            var actionResult = controller.AuthenticateAsync(user);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as User;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user.Username,actual.Username);
        }

        [Fact]
        public async void AuthenticationReturnsBadRequest()
        {
            var user = await GetSampleDetails();
            user.Username = "Vin";
            user.Password = "abcd";
            service.Setup(x => x.Authenticate("vinayak", "Vinayak123@")).Returns(GetSampleUserDetails);
            var controller = new UsersController(service.Object);

            //act
            var actionResult = controller.AuthenticateAsync(user );
            var result = actionResult.Result;

            //assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        private async Task<AuthenticationModel> GetSampleDetails()
        {
            AuthenticationModel output = new AuthenticationModel
                {
                    Username = "vinayak",
                    Password = "Vinayak123@",
                };
 
            return output;
        }

        private async Task<User> GetSampleUserDetails()
        {
            User output = new User
            {
                Email="vinayak@gmail.com",
                Username = "vinayak",
                Password = "Vinayak123@",
            };

            return output;
        }
    }
}
