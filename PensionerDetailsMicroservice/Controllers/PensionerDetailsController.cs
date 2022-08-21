using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionerDetailsMicroservice.Models;
using PensionerDetailsMicroservice.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailsMicroservice.Controllers
{
    [Route("api/PensionerDetails")]
    [ApiController]
    [Authorize]
    public class PensionerDetailsController : ControllerBase
    {
        private readonly IPensionerDetailsRepository _pensionerDetailRepo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerDetailsController));

        public PensionerDetailsController(IPensionerDetailsRepository pensionerDetailRepo)
        {
            _pensionerDetailRepo = pensionerDetailRepo;
        }

        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetDetailsAsync()
        {
            _log4net.Info(" Http GET Request From GetDetails method of: " + nameof(PensionerDetailsController));
            var path = @"PensionerDetails.csv";

            //Here We are calling function to read CSV file
            var resultData = await _pensionerDetailRepo.ReadCSVFile(path);

            return Ok(resultData);
        }

        [HttpGet("[action]/{aadharNumber}")]
        public async Task<IActionResult> GetPensionerDetailAsync(string aadharNumber)
        {
            _log4net.Info(" Http GET Request From GetPensionerDetails method of: " + nameof(PensionerDetailsController));
            var path = @"PensionerDetails.csv";

            //Here We are calling function to read CSV file
            var resultData = await _pensionerDetailRepo.ReadCSVFile(path);

            //var pensionerDetail = resultData.Where(item => item.AadharNumber == aadharNumber).Select(item=>item);
            var pensionerDetail = resultData.FirstOrDefault(item => item.AadharNumber == aadharNumber);
            if (pensionerDetail != null && resultData.Any())
            {
                _log4net.Info("Pensioner details returned From GetDetails method of: " + nameof(PensionerDetailsController));
                return Ok(pensionerDetail);
            }
            _log4net.Info("BadRequest returned From GetDetails method of: " + nameof(PensionerDetailsController));
            return BadRequest(new { message = "Aaadhar number is invalid" });
        }

        //[HttpPost("PostDetail")]
        //public IActionResult PostDeatil(PensionerDetail pensionerDetail)
        //{
        //    var path = @"C:\Users\912682\source\repos\AuthenticationTrial\PensionerDetails.csv";

        //    //Here We are calling function to read CSV file
        //    var resultData = _pensionerDetailRepo.ReadCSVFile(path);

        //    resultData.Add(pensionerDetail);
        //    //Here We are calling function to write file

        //    _pensionerDetailRepo.WriteCSVFile(path, resultData);
        //    return Ok(resultData);
        //}
    }
}
