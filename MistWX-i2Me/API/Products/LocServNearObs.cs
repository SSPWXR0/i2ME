using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.API.Products;

public class LocServNearObs : Base
{
    public LocServNearObs()
    {
        RecordName = "LocServNearObs";
        DataUrl =
            "https://api.weather.com/v3/location/near?geocode={lat},{long}&product=observation&format=json&apiKey={apiKey}";
    }

    public async Task<List<GenericResponse<LocServNearObsResponse>>> Populate(string[] locations)
    {
        return await GetJsonData<LocServNearObsResponse>(locations);
    }
}
