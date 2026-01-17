using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.API.Products;

public class LocServPointProduct : Base
{
    public LocServPointProduct()
    {
        RecordName = "LocServPoint";
        DataUrl =
            "https://api.weather.com/v3/location/point?locid={locId}:{locType}:{cntryCd}&language={lang}&format=json&apiKey={apiKey}";
    }

    public async Task<List<GenericResponse<LocServPointResponse>>> Populate(string[] locations)
    {
        return await GetJsonData<LocServPointResponse>(locations);
    }
}
