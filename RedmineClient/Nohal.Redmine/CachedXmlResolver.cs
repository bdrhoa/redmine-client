// <copyright file="XhtmlPage.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-29</date>
// <summary>Class representing the XHTML page.
// Taken from http://www.codewrecks.com/blog/index.php/2008/05/20/xmldocument-xmlresolver-and-cache-the-schema-of-xhtml/ </summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Nohal.Redmine
{
    public class CachedXmlResolver : XmlUrlResolver
    {
        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            if (relativeUri == "-//W3C//DTD XHTML 1.0 Strict//EN")
                return new Uri("http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd");
            else if (relativeUri == "-//W3C XHTML 1.0 Transitional//EN")
                return new Uri("http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd");
            else if (relativeUri == "-//W3C//DTD XHTML 1.0 Transitional//EN")
                return new Uri("http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd");
            else if (relativeUri == "-//W3C XHTML 1.0 Frameset//EN")
                return new Uri("http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd");
            else if (relativeUri == "-//W3C//DTD XHTML 1.1//EN")
                return new Uri("http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd");
            return base.ResolveUri(baseUri, relativeUri);
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            if (!cache.ContainsKey(absoluteUri))
                GetNewStream(absoluteUri, role, ofObjectToReturn);
            return new FileStream(cache[absoluteUri], FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        private void GetNewStream(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            using (Stream stream = (Stream)base.GetEntity(absoluteUri, role, ofObjectToReturn))
            {
                String filename = System.IO.Path.GetTempFileName();
                using (FileStream ms = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    Byte[] buffer = new byte[8192];
                    Int32 count = 0;
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, count);
                    }
                    ms.Flush();
                    cache.Add(absoluteUri, filename);
                }
            }
        }

        public static Dictionary<Uri, String> cache = new Dictionary<Uri, String>();

    }
}
