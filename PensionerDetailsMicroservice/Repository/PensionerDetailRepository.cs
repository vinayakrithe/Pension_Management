using CsvHelper;
using PensionerDetailsMicroservice.Mappers;
using PensionerDetailsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionerDetailsMicroservice.Repository
{
    public class PensionerDetailRepository : IPensionerDetailsRepository
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerDetailRepository));

        public async Task<List<PensionerDetail>> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                {

                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.RegisterClassMap<PensionerDetailMap>();
                        var records = csv.GetRecords<PensionerDetail>().ToList();
                        _log4net.Info("Pensioner details feched in ReadCSVFile method of: " + nameof(PensionerDetailRepository));
                        return records;
                    }
                }
            }
            catch (Exception e)
            {
                _log4net.Error(" Error in reading CSV file From GetDetails method of: " + nameof(PensionerDetailRepository));
                throw new Exception(e.Message);
            }
        }

        //public void WriteCSVFile(string path, List<PensionerDetail> pensionerDetail)
        //{
        //    using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
        //    {
        //        using (CsvWriter cw = new CsvWriter(sw))
        //        {
        //            cw.WriteHeader<PensionerDetail>();
        //            cw.NextRecord();
        //            foreach (PensionerDetail stu in pensionerDetail)
        //            {
        //                cw.WriteRecord<PensionerDetail>(stu);
        //                cw.NextRecord();
        //            }
        //        }
        //    }
        //}
    }
}
