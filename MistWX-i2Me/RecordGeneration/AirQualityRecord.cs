using MistWX_i2Me.API;
using MistWX_i2Me.Schema.ibm;
using System.Xml.Serialization;
using System.Xml;

namespace MistWX_i2Me.RecordGeneration;

public class AirQualityRecord : I2Record
{
    public async Task<string> MakeRecord(List<GenericResponse<AirQualityResponse>> results)
    {
        Log.Info("Creating Air Quality Record");
        string recordPath = Path.Combine(AppContext.BaseDirectory, "temp", "AirQuality.xml");
        string recordScript = "<Data type=\"AirQuality\">";

        foreach (var result in results)
        {
            if (String.IsNullOrEmpty(result.Location.epaId))
            {
                continue;
            }
            
            if (result.ParsedData.Airquality != null)
            {
                if (result.ParsedData.Airquality.Airqualityreport != null)
                {
                    foreach (var report in result.ParsedData.Airquality.Airqualityreport)
                    {
                        XmlSerializer mdserializer = new XmlSerializer(typeof(AirQualityMetadata));
                        XmlSerializer fcserializer = new XmlSerializer(typeof(Airqualityreport));
                        StringWriter sw = new StringWriter();
                        XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings
                        {
                            OmitXmlDeclaration = true,
                            ConformanceLevel = ConformanceLevel.Fragment, 
                        });

                        xw.WriteWhitespace("");

                        if (result.ParsedData.Metadata != null)
                        {
                            result.ParsedData.Metadata.ClientKey = result.Location.epaId;
                            result.ParsedData.Metadata.Date = DateTime.Now.ToString("yyyyMMdd");
                            mdserializer.Serialize(xw, result.ParsedData.Metadata);
                        }

                        if (report != null)
                        {
                            fcserializer.Serialize(xw, report);
                        }
                        
                        
                        recordScript +=
                            $"<AirQuality>" +
                            $"{sw}</AirQuality>";
                    }
                }
            }
        }
        
        recordScript += "</Data>";

        File.WriteAllText(recordPath, ValidateXml(recordScript));
        
        return recordPath;
    }
}