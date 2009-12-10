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
        /// Initializes a new instance of the XhtmlPage class.
        /// </summary>
        internal XhtmlPage()
        {
            this.xml = new XmlDocument();
        }

        /// <summary>
        /// Initializes a new instance of the XhtmlPage class.
        /// Creates object from the supplied text.
        /// </summary>
        /// <param name="pageContent">Text of the page</param>
        internal XhtmlPage(string pageContent) : this()
        {
            //this is an ugly hack for W3C not allowing the XmlResolver to download the DTDs
            string pageText = pageContent.Replace("&nbsp;", " ").Replace("&copy;", "(c)");
            this.xml.XmlResolver = null; 

            this.xml.LoadXml(pageText);
            this.xml.XmlResolver = new XmlUrlResolver();
        }

        /// <summary>
        /// Initializes a new instance of the XhtmlPage class.
        /// Creates object from the supplied stream.
        /// </summary>
        /// <param name="pageContent">Stream providing the text of the page</param>
        internal XhtmlPage(Stream pageContent) : this()
        {
            this.xml.Load(pageContent);
        }

        /// <summary>
        /// Gets the page XmlDocument
        /// </summary>
        internal XmlDocument XmlDocument
        {
            get
            {
                return this.xml;
            }
        }

        /// <summary>
        /// Gets the System.Xml.XmlNode with the specified ID
        /// </summary>
        /// <param name="elementId">The element ID to match</param>
        /// <returns>Matching System.Xml.XmlNode</returns>
        internal XmlNode GetElementById(string elementId)
        {
            return this.xml.GetElementById(elementId);
        }

        /// <summary>
        /// Gets the collection of possible values from the checkboxes contained in XmlNodeList.
        /// The expected xhtml is for example: &lt;label class="floating" &gt;&lt;input id="issue[watcher_user_ids][]"  name="issue[watcher_user_ids][]"  type="checkbox"  value="3" /&gt; test user&lt;/label&gt;
        /// </summary>
        /// <param name="checkboxId">Id of the checkboxes</param>
        /// <returns>collection of key->value pairs</returns>
        internal Dictionary<int, string> GetCheckBoxOptions(string checkboxId)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            foreach (XmlNode node in this.GetElementsByName("input"))
            {
                if (node.Attributes["type"].Value == "checkbox" && node.Attributes["id"].Value == checkboxId)
                {
                    dict.Add(Convert.ToInt32(node.Attributes["value"].Value), node.ParentNode.InnerText.Trim());   
                }
            }

            return dict;
        }

        /// <summary>
        /// Gets the collection of possible values from the select with specified Id
        /// </summary>
        /// <param name="selectId">ID of the select element in the page</param>
        /// <returns>collection of key->value pairs</returns>
        internal Dictionary<int, string> GetSelectOptions(string selectId)
        {
            XmlNode selectNode = this.GetElementById(selectId);
            if (selectNode == null)
            {
                XmlNodeList nodes = this.GetElementsByName("select");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["id"] != null && node.Attributes["id"].Value == selectId)
                    {
                        selectNode = node;
                    }
                }
            }
            return this.ParseSelect(selectNode);
        }

        /// <summary>
        /// Gets the System.Xml.XmlNodeList of nodes with the specified name
        /// </summary>
        /// <param name="elementName">The name of the element</param>
        /// <returns>Matching System.Xml.XmlNodeList</returns>
        private XmlNodeList GetElementsByName(string elementName)
        {
            return this.xml.GetElementsByTagName(elementName);
        }

        /// <summary>
        /// Parses possible values of XHTML select into a collection of key->value pairs
        /// </summary>
        /// <param name="selectNode">DOM node representing combobox</param>
        /// <returns>Collection of Key->Value pairs</returns>
        private Dictionary<int, string> ParseSelect(XmlNode selectNode)
        {
            Dictionary<int, string> parsed = new Dictionary<int, string>();
            if (selectNode != null)
            {
                foreach (XmlNode list in selectNode.ChildNodes)
                {
                    if (list.Attributes["value"].Value != String.Empty)
                    {
                        parsed.Add(Convert.ToInt32(list.Attributes["value"].Value), list.InnerText);
                    }
                }
            }

            return parsed;
        }
    }
}
