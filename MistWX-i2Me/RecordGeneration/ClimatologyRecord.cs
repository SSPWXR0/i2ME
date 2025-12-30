using System.Xml;
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
            cliRec.Loc = result.Location.cliStn;
            if (result.ParsedData.temperatureAverageMax != null)
            {
                if (result.ParsedData.temperatureAverageMax.First() != null)
                {
                    cliRec.AvgHigh = Convert.ToInt32(result.ParsedData.temperatureAverageMax.First());
                }
                
            }
            if (result.ParsedData.temperatureAverageMin != null)
            {
                if (result.ParsedData.temperatureAverageMin.First() != null)
                {
                    cliRec.AvgLow = Convert.ToInt32(result.ParsedData.temperatureAverageMin.First());
                }
                
            }
            if (result.ParsedData.temperatureRecordMax != null)
            {
                if (result.ParsedData.temperatureRecordMax.First() != null)
                {
                    cliRec.RecHigh = Convert.ToInt32(result.ParsedData.temperatureRecordMax.First());
                }
                
            }
            if (result.ParsedData.temperatureRecordMin != null)
            {
                if (result.ParsedData.temperatureRecordMin.First() != null)
                {
                    cliRec.RecLow = Convert.ToInt32(result.ParsedData.temperatureRecordMin.First());
                }
                
            }
            if (result.ParsedData.almanacRecordYearMax != null)
            {
                if (result.ParsedData.almanacRecordYearMax.First() != null)
                {
                    cliRec.RecHighYear = Convert.ToInt32(result.ParsedData.almanacRecordYearMax.First());
                }
                
            }
            if (result.ParsedData.almanacRecordYearMin != null)
            {
                if (result.ParsedData.almanacRecordYearMin.First() != null)
                {
                    cliRec.RecLowYear = Convert.ToInt32(result.ParsedData.almanacRecordYearMin.First());
                }
            }
            
            cliRec.Year = System.DateTime.Now.Year;
            cliRec.Month = System.DateTime.Now.Month;
            cliRec.Day = System.DateTime.Now.Day;
            cliRecRes.ClimoRec = cliRec;

            XmlSerializer serializer = new XmlSerializer(typeof(ClimatologyRecordResponse));
            StringWriter sw = new StringWriter();
            XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment, 
            });
            xw.WriteWhitespace("");
            serializer.Serialize(xw, cliRecRes);
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