using System.Security.Policy;
using System.Xml.Serialization;

namespace MistWX_i2Me.Schema.twc;

[XmlRoot(ElementName="AirportDelayIndexes")]
public class AirportDelayIndexes
{
    [XmlAttribute(AttributeName="key")]
    public string? Key { get; set; }

    [XmlAttribute(AttributeName="indexes")]
    public string? Indexes { get; set; }
}