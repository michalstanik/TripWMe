using System.Collections.Generic;
using System.Xml.Serialization;

namespace TripWMe.Data.SampleData
{
    [XmlRoot(ElementName = "row")]
    public class Row
    {
        [XmlElement(ElementName = "id_number")]
        public string Id_number { get; set; }
        [XmlElement(ElementName = "image_url")]
        public string Image_url { get; set; }
        [XmlElement(ElementName = "iso_code")]
        public string Iso_code { get; set; }
        [XmlElement(ElementName = "latitude")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "location")]
        public string Location { get; set; }
        [XmlElement(ElementName = "longitude")]
        public string Longitude { get; set; }
        [XmlElement(ElementName = "region")]
        public string Region { get; set; }
    }

    [XmlRoot(ElementName = "rows")]
    public class Rows
    {
        [XmlElement(ElementName = "row")]
        public List<Row> Row { get; set; }
    }
}
