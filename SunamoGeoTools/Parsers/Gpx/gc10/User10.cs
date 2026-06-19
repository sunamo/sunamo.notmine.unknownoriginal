namespace csGeoTools.Parsers.gpx.gc10
{
    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0")]
    public class User10
    {
        [XmlTextAttribute()]
        public String Name { get; set; }
        [XmlAttribute("id")]
        public String Id { get; set; }
    }
}
