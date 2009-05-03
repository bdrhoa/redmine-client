// <copyright file="XhtmlPage.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-29</date>
// <summary>Class representing the XHTML page.</summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the XHTML page
    /// </summary>
    internal class XhtmlPage
    {
        /// <summary>
        /// The page document
        /// </summary>
        private XmlDocument xml;

        /// <summary>
        /// Gets the page XmlDocument
        /// </summary>
        internal XmlDocument XmlDocument
        {
            get
            {
                return xml;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private XhtmlPage()
        {
            this.xml = new XmlDocument();
        }

        /// <summary>
        /// Creates object from the text
        /// </summary>
        /// <param name="pageContent">Text of the page</param>
        internal XhtmlPage(string pageContent) : this()
        {
            xml.LoadXml(pageContent);
        }

        /// <summary>
        /// Creates object from the text
        /// </summary>
        /// <param name="pageContent">Stream providing the text of the page</param>
        internal XhtmlPage(Stream pageContent) : this()
        {
            xml.Load(pageContent);
        }

        /// <summary>
        /// Gets the System.Xml.XmlElement with the specified ID
        /// </summary>
        /// <param name="elementId">The element ID to match</param>
        /// <returns>Matching System.Xml.XmlNode</returns>
        internal XmlNode GetElementById(string elementId)
        {
            return xml.GetElementById(elementId);
        }

        /// <summary>
        /// Gets the collection of possible values from the select with specified Id
        /// </summary>
        /// <param name="selectId">ID of the select element in the page</param>
        /// <returns>collection of key->value pairs</returns>
        internal List<KeyValuePair<int, string>> GetSelectOptions(string selectId)
        {
            return ParseSelect(GetElementById(selectId));
        }

        /// <summary>
        /// Parses possible values of XHTML select into a collection of key->value pairs
        /// </summary>
        /// <param name="selectNode">DOM node representing combobox</param>
        /// <returns>Collection of Key->Value pairs</returns>
        private List<KeyValuePair<int, string>> ParseSelect(XmlNode selectNode)
        {
            List<KeyValuePair<int, string>> parsed = new List<KeyValuePair<int, string>>();
            if (selectNode != null)
            {
                foreach (XmlNode list in selectNode.ChildNodes)
                {
                    if (list.Attributes["value"].Value != String.Empty)
                    {
                        parsed.Add(new KeyValuePair<int, string>(Convert.ToInt32(list.Attributes["value"].Value), list.InnerText));
                    }
                }
            }
            return parsed;
        }
    }
}
