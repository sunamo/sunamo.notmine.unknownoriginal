namespace csGeoTools.Parsers.gpx.gc101
{
    [XmlTypeAttribute(Namespace = "http://www.groundspeak.com/cache/1/0/1")]
    public class User101
    {
        [XmlTextAttribute()]
        public String Name { get; set; }
        [XmlAttribute("id")]
        public String Id { get; set; }
    }
}
