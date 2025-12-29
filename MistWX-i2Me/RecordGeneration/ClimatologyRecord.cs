using System.Xml.Serialization;
using MistWX_i2Me.API;
using MistWX_i2Me.Schema.ibm;
using MistWX_i2Me.Schema.twc;

namespace MistWX_i2Me.RecordGeneration;

public class ClimatologyRecord : I2Record
{
    public async Task<string> MakeRecord(List<GenericResponse<Almanac1DayResponse>> results)
    {
        Log.Info("Creating Climatology Record.");
        string recordPath = Path.Combine(AppContext.BaseDirectory, "temp", "ClimatologyRecord.xml");
        string recordScript = "<Data type=\"ClimatologyRecord\">";

        foreach (var result in results)
        {
            ClimatologyRecordResponse cliRecRes = new ClimatologyRecordResponse();
            ClimatologyRec cliRec = new ClimatologyRec();
            cliRecRes.Key = result.Location.cliStn;
            cliRec.AvgHigh = result.ParsedData.temperatureAverageMax.First();
            cliRec.AvgLow = result.ParsedData.temperatureAverageMin.First();
            cliRec.RecHigh = result.ParsedData.temperatureRecordMax.First();
            cliRec.RecLow = result.ParsedData.temperatureRecordMin.First();
            cliRec.RecHighYear = result.ParsedData.almanacRecordYearMax.First();
            cliRec.RecLowYear = result.ParsedData.almanacRecordYearMin.First();
            cliRec.Month = System.DateTime.Now.Month;
            cliRec.Day = System.DateTime.Now.Day;
            cliRecRes.ClimoRec = cliRec;

            XmlSerializer serializer = new XmlSerializer(typeof(ClimatologyRecordResponse));
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, cliRecRes);
            sw.Close();

            recordScript += 
                $"<ClimatologyRecord>" +
                $"<Key>{result.Location.cliStn}</Key>{sw.ToString()}</ClimatologyRecord>";
        }
        
        recordScript += "</Data>";
        
        await File.WriteAllTextAsync(recordPath, ValidateXml(recordScript));

        return recordPath;
    }
}