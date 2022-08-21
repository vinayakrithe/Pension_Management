using CsvHelper.Configuration;
using PensionerDetailsMicroservice.Models;

namespace PensionerDetailsMicroservice.Mappers
{
    public sealed class PensionerDetailMap : ClassMap<PensionerDetail>
    {
        public PensionerDetailMap()
        {
            Map(x => x.AadharNumber).Name("AadharNumber");
            Map(x => x.Name).Name("Name");
            Map(x => x.DateOfBirth).Name("DateOfBirth");
            Map(x => x.PAN).Name("PAN");
            Map(x => x.SalaryEarned).Name("SalaryEarned");
            Map(x => x.Allowances).Name("Allowances");
            Map(x => x.PensionType).Name("PensionType");
            Map(x => x.BankName).Name("BankName");
            Map(x => x.AccountNumber).Name("AccountNumber");
            Map(x => x.BankType).Name("BankType");
        }
    }
}
