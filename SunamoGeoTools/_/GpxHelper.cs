namespace SunamoGpx
{
    public class GpxHelper
    {
        /// <summary>
        /// A1 must be object to pass stream from wpf and uwp
        /// </summary>
        /// <param name="content"></param>
        public static List<Waypoint10> ParseGpxFile(string content)
        {
            //HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
            //hd.LoadHtml(TF.ReadAllText(file));

            //var wpts = HtmlAgilityHelper.Nodes(hd.DocumentNode, true, "wpt");
            //foreach (var item in wpts)
            //{
            //    var sym = HtmlAssistant.InnerText(item, false, "sym");
            //    if (sym == "Geocache")
            //    {

            //    }
            //}

            List<Waypoint10> wpt = new List<Waypoint10>();

            Type gpxType = typeof(csGeoTools.Parsers.gpx.gpx10.Gpx);
            XmlSerializer ser = new XmlSerializer(gpxType);
            csGeoTools.Parsers.gpx.gpx10.Gpx gpx;
            using (XmlReader reader = XmlReader.Create(new StringReader(content)))
            {
                // Cant be use in UWP - in classic windows this project is good. In UWP parses nothing.
                gpx = (csGeoTools.Parsers.gpx.gpx10.Gpx)ser.Deserialize(reader);
            }
            foreach (var item in gpx.Waypoints)
            {
                string type = item.Type;
                if (type.StartsWith("Geocache|"))
                {
                    wpt.Add(item);
                }
            }

            return wpt;
        }
    }
}
