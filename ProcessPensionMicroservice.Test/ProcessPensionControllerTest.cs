using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProcessPensionMicroservice.Controllers;
using ProcessPensionMicroservice.Models;
using ProcessPensionMicroservice.Repository.IRepository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProcessPensionMicroservice.Test
{
    public class ProcessPensionControllerTest
    {
        private readonly Mock<IProcessPensionRepository> service;

        public ProcessPensionControllerTest()
        {
            service = new Mock<IProcessPensionRepository>();
        }

        [Fact]
        public async Task PostPensionDetailsWorks()
        {
            var pensioner = await GetSampleDetailsAsync();
            service.Setup(x => x.GetPensionerDetailAsync("123", It.IsAny<string>())).Returns(GetSampleDetailsAsync());
            service.Setup(x => x.CalculatePensionDetail(It.IsAny<PensionerDetail>())).Returns(GetSampleDetails());

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE2NjA0NjAyMjksImV4cCI6MTY2MTA2NTAyOSwiaWF0IjoxNjYwNDYwMjI5fQ._4oyjhszXnOhSXnmBF2BhLAvAIYDRRx7UdoS6HPrSKU";

            var controller = new ProcessPensionController(service.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };
            //act
            var actionResult = controller.PensionDetail(pensioner.AadharNumber);
            service.VerifyAll();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as PensionerDetail;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pensioner.PensionAmount, actual.PensionAmount);
        }

        [Fact]
        public async Task PostPensionDetailsFails()
        {
            var aadharNo = "456";
            service.Setup(x => x.GetPensionerDetailAsync("123", It.IsAny<string>())).Returns(GetSampleDetailsAsync());

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE2NjA0NjAyMjksImV4cCI6MTY2MTA2NTAyOSwiaWF0IjoxNjYwNDYwMjI5fQ._4oyjhszXnOhSXnmBF2BhLAvAIYDRRx7UdoS6HPrSKU";

            var controller = new ProcessPensionController(service.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //act
            var actionResult = controller.PensionDetail(aadharNo);
            var result = actionResult.Result;

            //assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        private async Task<PensionerDetail> GetSampleDetailsAsync()
        {
            PensionerDetail output = new PensionerDetail
            {
                AadharNumber = "123",
                Name = "Usama",
                DateOfBirth = DateTime.Now,
                PAN = "ABC123",
                SalaryEarned = 40000,
                Allowances = 500,
                PensionType = "self",
                BankName = "SBI",
                AccountNumber = "123abcd",
                BankType = "Public",
                PensionAmount =32500,
                BankServiceCharge = 500,
            };
            return output;
        }

        private PensionerDetail GetSampleDetails()
        {
            PensionerDetail output = new PensionerDetail
            {
                AadharNumber = "123",
                Name = "Usama",
                DateOfBirth = DateTime.Now,
                PAN = "ABC123",
                SalaryEarned = 40000,
                Allowances = 500,
                PensionType = "self",
                BankName = "SBI",
                AccountNumber = "123abcd",
                BankType = "Public",
                PensionAmount = 32500,
                BankServiceCharge = 500,
            };
            return output;
        }
    }
}
