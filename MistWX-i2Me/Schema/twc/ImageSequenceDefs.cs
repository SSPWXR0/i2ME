using System.Xml.Serialization;
using MistWX_i2Me.RecordGeneration;

namespace MistWX_i2Me.Schema.twc;

[XmlRoot(ElementName="ImageSequenceDef")]
public class ImageSequenceDef { 

	[XmlAttribute(AttributeName="type")] 
	public string? type { get; set; } 

    [XmlAttribute(AttributeName="area")] 
	public string? area { get; set; } 

    [XmlElement(ElementName="LowerLeftLong")]
    public float LowerLeftLong { get; set; }

    [XmlElement(ElementName="LowerLeftLat")]
    public float LowerLeftLat { get; set; }

    [XmlElement(ElementName="UpperRightLong")]
    public float UpperRightLong { get; set; }

    [XmlElement(ElementName="UpperRightLat")]
    public float UpperRightLat { get; set; }

    [XmlElement(ElementName="VerticalAdjustment")]
    public float VerticalAdjustment { get; set; }

    [XmlElement(ElementName="OriginalImageWidth")]
    public int OriginalImageWidth { get; set; }

    [XmlElement(ElementName="OriginalImageHeight")]
    public int OriginalImageHeight { get; set; }

    [XmlElement(ElementName="MaxImages")]
    public int MaxImages { get; set; }

    [XmlElement(ElementName="Gap")]
    public int Gap { get; set; }

    [XmlElement(ElementName="ImagesInterval")]
    public int ImagesInterval { get; set; }

    [XmlElement(ElementName="Expiration")]
    public int Expiration { get; set; }

    [XmlElement(ElementName="DeletePadding")]
    public int DeletePadding { get; set; }

    [XmlElement(ElementName="FileNameDateFormat")]
    public string? FileNameDateFormat { get; set; }

    public Point<float> GrabUpperRight()
    {
        return new Point<float>(UpperRightLat, UpperRightLong);
    }

    public Point<float> GrabLowerLeft()
    {
        return new Point<float>(LowerLeftLat, LowerLeftLong);
    }

    public Point<float> GrabUpperLeft()
    {
        return new Point<float>(UpperRightLat, LowerLeftLong);
    }

    public Point<float> GrabLowerRight()
    {
        return new Point<float>(LowerLeftLat, UpperRightLong);
    }
    
}

[XmlRoot(ElementName="ImageSequenceDefs")]
public class ImageSequenceDefs
{
    [XmlElement(ElementName ="ImageSequenceDef")] 
	public List<ImageSequenceDef>? ImageSequenceDef { get; set; } 
}