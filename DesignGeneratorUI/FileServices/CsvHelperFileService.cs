using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    public class CsvHelperFileService : IFileService
    {
        public void OpenFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(string filename, IEnumerable<object> data)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            using (var writer = new StreamWriter(filename, false, new UTF8Encoding(true))) 
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                //csv.WriteHeader<Foo>();
                csv.WriteRecords(data);
            }
        }
    }
}
