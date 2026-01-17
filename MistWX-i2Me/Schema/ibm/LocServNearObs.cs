namespace MistWX_i2Me.Schema.ibm;

public class LocServNearObsLocation
{
    public List<string?> adminDistrictCode { get; set; }

    public List<string?> stationName { get; set; }

    public List<string?> countryCode { get; set; }

    public List<string?> stationId { get; set; }

    public List<string?> ianaTimeZone { get; set; }

    public List<string?> obsType { get; set; }

    public List<string> latitude { get; set; }

    public List<string> longitude { get; set; }

    public List<string?> distanceKm { get; set; }

    public List<string?> distanceMi { get; set; }
}

public class LocServNearObsResponse
{
    public LocServNearObsLocation location { get; set; }
}