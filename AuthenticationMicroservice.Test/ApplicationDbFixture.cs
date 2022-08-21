using AuthenticationMicroservice.Data;
using Microsoft.EntityFrameworkCore;
using AuthenticationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AuthenticationMicroservice;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AuthenticationMicroservice.Test
{
    public class ApplicationDbFixture
    {
        internal readonly ApplicationDbContext _db;
        public readonly IOptions<AppSettings> _config;
        public ApplicationDbFixture()
        {
            _db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("UserTestDb").Options);
            _db.Users.Add(new User() { Id = 1, Email = "vinayak@gmail.com", Username = "vinayak", Password = "Vinayak123@" });
            _db.SaveChanges();

            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .Build();

            _config = Options.Create(configuration.GetSection("AppSettings").Get<AppSettings>());
        }
    }
}
