using PensionerDetailsMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PensionerDetailsMicroservice.Test
{
    public class PensionerDetailRepoTest
    {
        private readonly IPensionerDetailsRepository _pensionerDetailsRepository;
        public PensionerDetailRepoTest()
        {
            _pensionerDetailsRepository = new PensionerDetailRepository();
        }

        [Fact]
        public async void ReadCSVFileWorks()
        {
            var expectedCount = 3;

            //act
            var result = await _pensionerDetailsRepository.ReadCSVFile(@"C:\Users\912682\OneDrive - Cognizant\Desktop\DummyPEnsionerDetails.csv");
            //assert
            Assert.Equal(expectedCount, result.Count);
        }
    }
}
