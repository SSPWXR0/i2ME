using MistWX_i2Me.API;
using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.RecordGeneration;

public class WateringNeedsRecord : I2Record
{
    public async Task<string> MakeRecord(List<GenericResponse<WateringNeedsResponse>> results)
    {
        Log.Info("Creating Watering Needs record.");
        string recordPath = Path.Combine(AppContext.BaseDirectory, "temp", "WateringNeeds.xml");
        string recordScript = "<Data type=\"WateringNeeds\">";

        foreach (var result in results)
        {
            recordScript += 
                $"<WateringNeeds id=\"000000000\" locationKey=\"{result.Location.coopId}\" isWxScan=\"0\">" +
                $"{result.RawResponse}<clientKey>{result.Location.coopId}</clientKey></WateringNeeds>";
        }
        
        recordScript += "</Data>";
        
        await File.WriteAllTextAsync(recordPath, ValidateXml(recordScript));

        return recordPath;
    }
}