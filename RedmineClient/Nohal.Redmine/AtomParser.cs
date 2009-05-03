// <copyright file="AtomParser.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Atom feed parser.</summary>
using System;
using System.Collections.Generic;
using System.Xml;

namespace Nohal.Redmine
{
    /// <summary>
    /// The Atom feed parser
    /// </summary>
    internal class AtomParser
    {
        /// <summary>
        /// Returns all the entries from an Atom feed
        /// </summary>
        /// <param name="feedXml">XML of the feed</param>
        /// <returns>List of all the entries</returns>
        protected internal static List<AtomEntry> ParseFeed(XmlDocument feedXml)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(feedXml.NameTable);
            nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");


            // successful
            if (feedXml.DocumentElement != null)
            {
                XmlNodeList titles = feedXml.DocumentElement.SelectNodes("//atom:entry/atom:title", nsmgr);
                XmlNodeList ids = feedXml.DocumentElement.SelectNodes("//atom:entry/atom:id", nsmgr);

                List<AtomEntry> entries = new List<AtomEntry>();

                if (titles != null && ids != null && ids.Count == titles.Count)
                {
                    for (int i = 0; i < titles.Count; i++)
                    {
                        entries.Add(new AtomEntry()
                                        {
                                            Id = ids[i].InnerText,
                                            Title = titles[i].InnerText
                                        });
                    }
                }

                return entries;
            }

            throw new ArgumentOutOfRangeException("feedXml", "No XML document provided");
        }
    }
}
