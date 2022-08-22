using Microsoft.AspNetCore.Mvc;
using Moq;
using PensionerDetailsMicroservice.Controllers;
using PensionerDetailsMicroservice.Models;
using PensionerDetailsMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PensionerDetailsMicroservice.Test
{
    public class PensionerDetailsControllerTest
    {
        //PensionerDetailsController _controller;
        private readonly Mock<IPensionerDetailsRepository> service;

        public PensionerDetailsControllerTest()
        {
            service = new Mock<IPensionerDetailsRepository>();
        }

        [Fact]
        public async void GetListOfPensioners()
        {
            var pensioner = await GetSampleDetails();
            service.Setup(x => x.ReadCSVFile(@"PensionerDetails.csv")).Returns(GetSampleDetails);
            var controller = new PensionerDetailsController(service.Object);

            //act
            var actionResult = controller.GetDetailsAsync();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as List<PensionerDetail>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pensioner.Count(), actual.Count());
        }

        [Fact]
        public async void GetPensionerByAadhar()
        {
            var pensioner = await GetSampleDetails();
            var firstPensioner = pensioner[0];
            service.Setup(x => x.ReadCSVFile(@"PensionerDetails.csv")).Returns(GetSampleDetails);
            var controller = new PensionerDetailsController(service.Object);

            //act
            var actionResult = controller.GetPensionerDetailAsync(firstPensioner.AadharNumber);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as PensionerDetail;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(actual.AadharNumber, firstPensioner.AadharNumber);
        }

        [Fact]
        public async void GetPensionerByAadhar_ReturnsBadRequest()
        {
            var pensioner = await GetSampleDetails();
            var firstPensioner = pensioner[0];
            service.Setup(x => x.ReadCSVFile(@"PensionerDetails.csv")).Returns(GetSampleDetails);
            var controller = new PensionerDetailsController(service.Object);

            //act
            var actionResult = controller.GetPensionerDetailAsync("sdwdf");
            var result = actionResult.Result;

            //assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        private async Task<List<PensionerDetail>> GetSampleDetails()
        {
            List<PensionerDetail> output = new List<PensionerDetail> 
            { 
                new PensionerDetail
                {
                    AadharNumber="123",
                    Name="Usama",
                    DateOfBirth=DateTime.Now,
                    PAN="ABC123",
                    SalaryEarned=40000,
                    Allowances=500,
                    PensionType="self",
                    BankName="SBI",
                    AccountNumber="123abcd",
                    BankType="Public",
                },
                new PensionerDetail
                {
                    AadharNumber="124",
                    Name="Vin",
                    DateOfBirth=DateTime.Now,
                    PAN="XYz123",
                    SalaryEarned=60000,
                    Allowances=550,
                    PensionType="family",
                    BankName="HDFC",
                    AccountNumber="567dvc",
                    BankType="Private",
                },
            };
            return output;
        }
        
    }
}
