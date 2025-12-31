using MistWX_i2Me.API;
using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.RecordGeneration;

public class PollenObservationsRecord : I2Record
{
    public async Task<string> MakeRecord(List<GenericResponse<PollenObservationsResponse>> results)
    {
        Log.Info("Creating current observations record..");
        string recordPath = Path.Combine(AppContext.BaseDirectory, "temp", "PollenObs.xml");
        string recordScript = "<Data type=\"PollenObs\">";

        foreach (var result in results)
        {
            recordScript +=
                $"<PollenObs id=\"000000000\" locationKey=\"{result.Location.primTecci}\" isWxScan=\"0\">" +
                $"{result.RawResponse}<clientKey>{result.Location.primTecci}</clientKey></PollenObs>";
        }
        
        recordScript += "</Data>";

        await File.WriteAllTextAsync(recordPath, ValidateXml(recordScript));

        return recordPath;
    }
}