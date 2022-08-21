using PensionerDetailsMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionerDetailsMicroservice.Repository
{
    public interface IPensionerDetailsRepository
    {
        Task<List<PensionerDetail>> ReadCSVFile(string location);
        //void WriteCSVFile(string path, List<PensionerDetail> pensionerDetail);
    }
}
