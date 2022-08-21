using ProcessPensionMicroservice.Models;
using ProcessPensionMicroservice.Repository;
using ProcessPensionMicroservice.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProcessPensionMicroservice.Test
{
    public class ProcessPensionRepositoryTest : IClassFixture<ApplicationDbFixture>
    {
        private readonly IProcessPensionRepository _callAPIRepository;
        public ProcessPensionRepositoryTest(ApplicationDbFixture applicationDbFixture)
        {
            _callAPIRepository = new ProcessPensionRepository(applicationDbFixture._db);
        }

        [Fact]
        public async void CalculatePensionDetailWorks()
        {
            var pensioner = await GetSampleDetails();

            //act
            var result = _callAPIRepository.CalculatePensionDetail(pensioner);

            //assert
            Assert.Equal(32500, result.PensionAmount);
        }

        [Fact]
        public void GetPensionerDetailFromDBWorks()
        {
            //act
            var result = _callAPIRepository.GetPensionerDetailFromDB("123");

            //assert
            Assert.Equal("Usama", result.Name);
        }

        [Fact]
        public void UpdatePensionerDetailWorks()
        {
            _callAPIRepository.Update(new PensionerDetail() {
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
            });

            var result = _callAPIRepository.GetPensionerDetailFromDB("123");

            Assert.Equal(32500 ,result.PensionAmount);
        }

        private async Task<PensionerDetail> GetSampleDetails()
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
                PensionAmount = 0,
                BankServiceCharge = 0,
            };
            return output;
        }
    }
}
