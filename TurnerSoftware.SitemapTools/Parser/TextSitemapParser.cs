namespace TurnerSoftware.SitemapTools.Parser
{
   
	public class TextSitemapParser : ISitemapParser
	{
        /// <summary>
        /// From original TurnerSoftware.SitemapTools.Parser
        /// parse from text file, on every line one address
        /// </summary>
        public SitemapFile ParseSitemap(TextReader reader)
		{
			var result = new SitemapFile();
			var line = string.Empty;

			var sitemapEntries = new List<SitemapEntry>();

			while ((line = reader.ReadLine()) != null)
			{
				if (Uri.TryCreate(line, UriKind.Absolute, out var tmpUri))
				{
					sitemapEntries.Add(new SitemapEntry
					{
						Location = tmpUri
					});
				}
			}

			return new SitemapFile
			{
				Urls = sitemapEntries
			};
		}

        public SitemapFile ParseSitemapXml(TextReader reader)
        {
            var text = reader.ReadToEnd();
            return ParseSitemapXml(text);
        }

        public SitemapFile ParseSitemapXml(string text)
        {
            

            XmlDocument urldoc = new XmlDocument();
            /*Load the downloaded string as XML*/
            urldoc.LoadXml(text);
            /*Create an list of XML nodes from the url nodes in the sitemap*/
            XmlNodeList xnList = urldoc.GetElementsByTagName("url");
            /*Loops through the node list and prints the values of each node*/
            
            

            var result = new SitemapFile();
            var line = string.Empty;

            var sitemapEntries = new List<SitemapEntry>();

            foreach (XmlNode node in xnList)
            {
                ChangeFrequency changeFrequency = ChangeFrequency.Monthly;
                var changefreq = node["changefreq"];
                if (changefreq != null)
                {
                    if (Enum.TryParse<ChangeFrequency>(changefreq.InnerText, out changeFrequency))
                    {

                    }
                }

                DateTime lastModified = DateTime.Now;
                var lastMod = node["lastmod"];
                if (lastMod != null)
                {
                    
                    if (DateTime.TryParse(lastMod.InnerText, out lastModified))
                    {

                    }
                }

                if (Uri.TryCreate(node["loc"].InnerText, UriKind.Absolute, out var tmpUri))
                {
                    var priority = node["priority"];

                    // Is not used in Window
                    //Priority = double.Parse(priority.InnerText),
                    //    LastModified = lastModified,
                    //    ChangeFrequency = changeFrequency

                    sitemapEntries.Add(new SitemapEntry
                    {
                        Location = tmpUri,
                        
                    });
                }
            }

            return new SitemapFile
            {
                Urls = sitemapEntries
            };
        }
    }
}
