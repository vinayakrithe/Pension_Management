using Microsoft.EntityFrameworkCore;
using ProcessPensionMicroservice.Data;
using ProcessPensionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessPensionMicroservice.Test
{
    public class ApplicationDbFixture
    {
        internal readonly ApplicationDbContext _db;
        public ApplicationDbFixture()
        {
            _db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("PensionerTestDb").Options);
            _db.PensionerDetails.Add(new PensionerDetail() {
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
            });
            _db.SaveChanges();
        }
    }
}
