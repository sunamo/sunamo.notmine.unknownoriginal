namespace csGeoTools.Parsers.gpx.gc101

    public class Description
    {
        [XmlAttribute("html")]
        public String _html { get; set; }
        public bool Html
        {
            get
            {
                return _html.Equals(true.ToString());
            }
            set
            {
                _html = value.ToString();
            }
        }
        [XmlTextAttribute()]
        public String value { get; set; }
        public String Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}
