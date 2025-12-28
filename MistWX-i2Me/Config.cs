using System.Xml.Serialization;

namespace MistWX_i2Me;

/// <summary>
/// Class for building the config.xml file used to configure the software
/// </summary>
[XmlRoot("Config")]
public class Config
{
    // Config Elements \\
    
    [XmlElement] public string TwcApiKey { get; set; } = "REPLACE_ME";
    [XmlElement] public string LogLevel { get; set; } = "info";
    [XmlElement] public bool GetAlerts { get; set; } = true;
    [XmlElement] public bool UseExecInstead { get; set; } = true;

    // Used to process what locations to generate
    [XmlElement]
    public string MachineProductConfig { get; set; } =
        "C:\\Program Files (x86)\\TWC\\i2\\Managed\\Config\\MachineProductCfg.xml";

    [XmlElement] public int RecordGenTimeSeconds { get; set; } = 3600;      // Defaults to 1 hour
    [XmlElement] public int CheckAlertTimeSeconds { get; set; } = 600;      // Defaults to 10 minutes
    
    [XmlElement] public LocConfig LocationConfig { get; set; } = new LocConfig();
    [XmlElement] public NetworkConfig UnitConfig { get; set; } = new NetworkConfig();
    [XmlElement("RadarConfig")] public RadarConfig RadarConfiguration { get; set; } = new RadarConfig();
    [XmlElement("DataConfig")] public DataEndpointConfig DataConfig { get; set; } = new DataEndpointConfig();

    // Actual configuration setup \\
    
    public static Config config = new Config();

    
    /// <summary>
    /// Loads values from the configuration file into the software
    /// </summary>
    /// <returns>Config object</returns>
    public static Config Load()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "config.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Config));
        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add("", "");
        
        // Create the temp directory if none exists
        if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, "temp")))
        {
            Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "temp"));
        }
        
        // Create a base config if none exists
        if (!File.Exists(path))
        {
            config = new Config();
            serializer.Serialize(File.Create(path), config, namespaces);

            return config;
        }

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            var deserializedConfig = serializer.Deserialize(stream);

            if (deserializedConfig != null && deserializedConfig is Config cfg)
            {
                config = cfg;
                return config;
            }

            return new Config();
        }
        
    }

    [XmlRoot("LocationConfig")]
    public class LocConfig
    {
        [XmlElement] public bool UseNationalLocations { get; set; } = false;
        [XmlArray("NationalLocations")] 
        [XmlArrayItem("Location")] 
        public string[] NationalLocations { get; set; } = {
                "USNM0004", "USGA0028", "USMD0018", "USME0017", "USMT0031", "USAL0054", "USND0037", "USID0025",
                "USMA0046", "USNY0081", "USVT0033", "USNC0121", "USIL0225", "USOH0188", "USOH0195", "USTX0327",
                "USCO0105", "USIA0231", "USMI0229", "USAZ0068", "USSC0140", "USCT0094", "USTX0617", "USIN0305",
                "USFL0228", "USMO0460", "USNV0049", "USAR0337", "USCA0638", "USKY1096", "USTN0325", "USFL0316",
                "USWI0455", "USMN0503", "USTN0357", "USLA0338", "USNY0996", "USNJ0355", "USVA0557", "USOK0400",
                "USNE0363", "USFL0372", "USPA1276", "USAZ0166", "USPA1290", "USME0328", "USOR0275", "USNC0558",
                "USSD0283", "USNV0076", "USCA0967", "USUT0225", "USTX1200", "USCA0982", "USCA0987", "USWA0395",
                "USWA0422", "USMO0787", "USFL0481", "USOK0537"
            };
        [XmlArray("LocationKeys")] 
        [XmlArrayItem("Key")] 
        public List<string> LocationKeys { get; set; } = new List<string> {
            "PrimaryLocation",
            "NearbyLocation1",
            "NearbyLocation2",
            "NearbyLocation3",
            "NearbyLocation4",
            "NearbyLocation5",
            "NearbyLocation6",
            "NearbyLocation7",
            "NearbyLocation8",
            "MetroMapCity1",
            "MetroMapCity2",
            "MetroMapCity3",
            "MetroMapCity4",
            "MetroMapCity5",
            "MetroMapCity6",
            "MetroMapCity7",
            "MetroMapCity8",
            "TideStation1",
            "TideStation2",
            "TideStation3",
            "TideStation4",
            "TideStation5",
            "TideStation6",
            "TideStation7",
            "TideStation8",
        };
    }

    [XmlRoot("UnitConfig")]
    public class NetworkConfig
    {
        [XmlElement] public int RoutineMsgPort { get; set; } = 7787;
        [XmlElement] public int PriorityMsgPort { get; set; } = 7788;
        [XmlElement] public string I2MsgAddress { get; set; } = "224.1.1.77";
        [XmlElement] public string InterfaceAddress { get; set; } = "127.0.0.1";
    }

    [XmlRoot("RadarConfig")]
    public class RadarConfig
    {
        [XmlElement] public bool UseRadarServer { get; set; } = false;
        [XmlElement] public string RadarServerUrl { get; set; } = "REPLACE_ME";
    }

    [XmlRoot("DataConfig")]
    public class DataEndpointConfig
    {
        [XmlElement] public bool CurrentConditions { get; set; } = true;
        [XmlElement] public bool MosquitoActivity { get; set; } = true;
        [XmlElement] public bool DrySkin { get; set; } = true;
        [XmlElement] public bool DailyForecast { get; set; } = true;
        [XmlElement] public bool HourlyForecast { get; set; } = true;
        [XmlElement] public bool AirQuality { get; set; } = false;
        [XmlElement] public bool AchesAndPains { get; set; } = true;
        [XmlElement] public bool Breathing { get; set; } = true;
        [XmlElement] public bool HeatingAndCooling { get; set; } = true;
        [XmlElement] public bool PollenForecast { get; set; } = true;
        [XmlElement] public bool WateringNeeds { get; set; } = true;
        [XmlElement] public bool TideForecast { get; set; } = true;
    }
}
