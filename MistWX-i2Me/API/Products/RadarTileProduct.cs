using MistWX_i2Me.Schema.ibm;
using NetVips;

namespace MistWX_i2Me.API.Products;

public class RadarTileProduct : Base
{
    public RadarTileProduct(int time, double x, double y, string radarType)
    {
        RecordName = "RadarTile";
        DataUrl =
            $"https://api.weather.com/v3/TileServer/tile?product={radarType}&ts={time}&xyz={x}:{y}:6&apiKey={{apikey}}";
    }

    public async Task<Image> Populate()
    {
        return await GetTileData();
    }
}
