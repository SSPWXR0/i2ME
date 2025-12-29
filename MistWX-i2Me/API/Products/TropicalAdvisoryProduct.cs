using MistWX_i2Me.Schema.ibm;

namespace MistWX_i2Me.API.Products;

public class TropicalAdvisoryProduct : Base
{
    public TropicalAdvisoryProduct()
    {
        RecordName = "TropicalAdvisory";
        DataUrl =
            "https://api.weather.com/v2/tropical/currentposition?source=default&basin=all&language=en-US&format=xml&units=e&nautical=true&apiKey={apiKey}";
    }

    public async Task<List<GenericResponse<TropicalAdvisoryResponse>>> Populate(string[] locations)
    {
        return await GetData<TropicalAdvisoryResponse>(locations);
    }
}