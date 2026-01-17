using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.API.Products;

public class LocServNearSki : Base
{
    public LocServNearSki()
    {
        RecordName = "LocServNearSki";
        DataUrl =
            "https://api.weather.com/v3/location/near?geocode={lat},{long}&product=ski&format=json&apiKey={apiKey}";
    }

    public async Task<List<GenericResponse<LocServNearSkiResponse>>> Populate(string[] locations)
    {
        return await GetJsonData<LocServNearSkiResponse>(locations);
    }
}
