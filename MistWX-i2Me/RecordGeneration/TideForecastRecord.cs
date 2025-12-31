using MistWX_i2Me.API;
using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.RecordGeneration;

public class TideForecastRecord : I2Record
{
    public async Task<string> MakeRecord(List<GenericResponse<TideForecastResponse>> results)
    {
        Log.Info("Creating Tide Forecast record..");
        string recordPath = Path.Combine(AppContext.BaseDirectory, "temp", "TideForecast.xml");
        string recordScript = "<Data type=\"TidesForecast\">";

        foreach (var result in results)
        {
            recordScript +=
                $"<TidesForecast id=\"000000000\" locationKey=\"{result.Location.primTecci}\" isWxScan=\"0\">" +
                $"{result.RawResponse}<clientKey>{result.Location.primTecci}</clientKey></TidesForecast>";
        }
        
        recordScript += "</Data>";

        await File.WriteAllTextAsync(recordPath, ValidateXml(recordScript));

        return recordPath;
    }
}