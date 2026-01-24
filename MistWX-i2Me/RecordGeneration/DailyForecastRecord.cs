using MistWX_i2Me.API;
using MistWX_i2Me.Schema.ibm;
using System.Xml.Serialization;
using System.Xml;

namespace MistWX_i2Me.RecordGeneration;

public class DailyForecastRecord : I2Record
{
    public static double CMtoIN(double cm)
    {
        return cm / 2.54;
    }
    public static double CtoF(double c)
    {
        return (c * (9/5)) + 32;
    }
    public static string QualifierCheck(double snow, int iconCode, double temp, double rain)
    {
        string UnitConfig = Config.config.LocalStarConfig.Unit;
        // Unit checks
        if (UnitConfig != "e")
        {
            snow = CMtoIN(snow);
            temp = CtoF(temp);
        }
        // Icon code checks 
        if (iconCode == 43)
        {
            return "Q9030";
        } else if (iconCode == 4)
        {
            return "Q9610";
        } else if (iconCode == 1)
        {
            return "Q9810";
        } else if (iconCode == 2)
        {
            return "Q9820";
        }
        
        // Rain checks 
        if (iconCode == 1 ||
            iconCode == 2 ||
            iconCode == 3 ||
            iconCode == 4 ||
            iconCode == 9 ||
            iconCode == 11 ||
            iconCode == 12)
        {
            if (rain > 0.3)
            {
                return "Q9410";
            } 
        }
        
        // Snow checks
        if (iconCode == 5 ||
            iconCode == 6 ||
            iconCode == 7 ||
            iconCode == 8 ||
            iconCode == 10 ||
            iconCode == 13 ||
            iconCode == 14 ||
            iconCode == 15 ||
            iconCode == 16 ||
            iconCode == 17 ||
            iconCode == 18 ||
            iconCode == 25 ||
            iconCode == 35 ||
            iconCode == 41 ||
            iconCode == 42 ||
            iconCode == 43 ||
            iconCode == 46 )
        {
            if (snow >= 3)
            {
                if (snow > 3 && snow <= 6)
                {
                    return "Q9010";
                } else if (snow > 6 && snow <= 12)
                {
                    return "Q9015";
                } else if (snow > 12)
                {
                    return "Q9020";
                }
                if (rain > 3 && rain <= 6)
                {
                    return "Q9010";
                } else if (rain > 6 && rain <= 12)
                {
                    return "Q9015";
                } else if (rain > 12)
                {
                    return "Q9020";
                }
                return "Q9005";
            }
        }
        
        // Temp checks
        if (temp <= 32){
            if (iconCode == 8)
            {
                return "Q9205";
            } else if (iconCode == 10)
            {
                return "Q9210";
            }
        }
        return "";
    }
    public async Task<string> MakeRecord(List<GenericResponse<DailyForecastResponse>> results)
    {
        Log.Info("Creating Daily Forecast Record");
        string recordPath = Path.Combine(AppContext.BaseDirectory, "temp", "DailyForecast.xml");
        string recordScript = "<Data type=\"DailyForecast\">";

        foreach (var result in results)
        {
            XmlSerializer mdserializer = new XmlSerializer(typeof(DailyForecastMetadata));
            XmlSerializer fcserializer = new XmlSerializer(typeof(Forecasts));
            StringWriter sw = new StringWriter();
            XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment, 
            });
            
            // check for potential severe weather qualifiers
            if (result.ParsedData.Forecasts != null)
            {  
                if (result.ParsedData.Forecasts.Forecast != null)
                {
                    foreach (Forecast fcst in result.ParsedData.Forecasts.Forecast)
                    {
                        fcst.QualifierCode = QualifierCheck(fcst.SnowQpf,0, fcst.MinTemp, fcst.Qpf);
                        if (fcst.Day != null)
                        {
                            fcst.Day.QualifierCode = QualifierCheck(fcst.Day.SnowQpf, fcst.Day.IconCode, fcst.Day.Temp, fcst.Day.Qpf);
                        }
                        if (fcst.Night != null)
                        {
                            fcst.Night.QualifierCode = QualifierCheck(fcst.Night.SnowQpf, fcst.Night.IconCode, fcst.Night.Temp, fcst.Night.Qpf);
                        }
                    }
                } 
            }
            
            xw.WriteWhitespace("");
            mdserializer.Serialize(xw, result.ParsedData.Metadata);
            fcserializer.Serialize(xw, result.ParsedData.Forecasts);

            recordScript +=
                $"<DailyForecast id=\"000000000\" locationKey=\"{result.Location.coopId}\" isWxScan=\"0\">" +
                $"{sw}<clientKey>{result.Location.coopId}</clientKey></DailyForecast>";
        }

        recordScript += "</Data>";
        
        await File.WriteAllTextAsync(recordPath, ValidateXml(recordScript));

        return recordPath;
    }
}