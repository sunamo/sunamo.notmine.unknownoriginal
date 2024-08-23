namespace csGeoTools.Parsers.gpx.gpx10

    public class Gpx
    {
        [XmlElement("name")]
        public String Name { get; set; }
        [XmlElement("desc")]
        public String Description { get; set; }
        [XmlElement("author")]
        public String Author { get; set; }
        [XmlElement("email")]
        public String Emailadress { get; set; }
        [XmlElement(DataType = "anyURI")]
        public String Url { get; set; }
        [XmlElement("urlname")]
        public String Urlname { get; set; }
        [XmlElement("time")]
        public String _time { get; set; }
        public DateTime Time
        {
            get
            {
                return DateTime.ParseExact(_time, DateFormats.KNOWN_FORMATS, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
            }
            set
            {
                _time = value.ToString(DateFormats.KNOWN_FORMATS.First());
            }
        }
        [XmlElement("keywords")]
        public String Keywords { get; set; }
        [XmlElement("bounds")]
        public Bounds Bounds { get; set; }
        [XmlElement("wpt")]
        public Waypoint10[] Waypoints { get; set; }
        [XmlElement("rte")]
        public Route[] Routes { get; set; }
        [XmlElement("trk")]
        public Track[] Tracks { get; set; }
        [XmlAttribute("version")]
        public String Version { get; set; }
        [XmlAttribute("creator")]
        public String Creator { get; set; }

        public Gpx()
        {
            this.Version = "1.0";
        }
    }
}
