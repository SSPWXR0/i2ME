namespace MistWX_i2Me.Schema.ibm;

public class LocServNearSkiLocation
{
    public List<string?> adminDistrictCode { get; set; }

    public List<string?> countryCode { get; set; }

    public List<string?> distanceKm { get; set; }

    public List<string?> distanceMi { get; set; }

    public List<string?> ianaTimeZone { get; set; }

    public List<string> latitude { get; set; }

    public List<string> longitude { get; set; }

    public List<string?> skiId { get; set; }

    public List<string?> skiName { get; set; }
}

public class LocServNearSkiResponse
{
    public LocServNearSkiLocation location { get; set; }
}