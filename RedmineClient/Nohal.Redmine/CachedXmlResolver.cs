// <copyright file="CachedXmlResolver.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-12-22</date>
// <summary>XmlResolver which uses DTDs from the resources instead of the W3C ones.
// Taken from http://sticklebackplastic.com/post/2007/07/03/XmlResolver-reading-from-resources.aspx </summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Nohal.Redmine
{

    internal sealed class XmlResourceResolver : XmlUrlResolver
    {

        private readonly Dictionary<string, Resource> _publicIdResources = new Dictionary<string, Resource>();
        private readonly Dictionary<string, Assembly> _assemblyMapping = new Dictionary<string, Assembly>();

        private class Resource
        {
            private readonly Assembly ResourceAssembly;
            private readonly string _resourceNameAsPath;

            public Resource(Assembly resourceAssembly, string resourceNameAsPath)
            {
                ResourceAssembly = resourceAssembly;
                _resourceNameAsPath = resourceNameAsPath;
            }

            public Uri AbsoluteUri
            {
                get
                {
                    UriBuilder builder = new UriBuilder();
                    builder.Scheme = ResourceScheme;
                    builder.Host = ResourceAssembly.GetName().Name.ToLower();
                    builder.Path = _resourceNameAsPath;

                    return builder.Uri;
                }
            }
        }

        internal void AddPublicIdMapping(string publicId, Assembly resourceAssembly, string resourceNamespace, string resourceName)
        {

            // Treat the resource namespace as a path - replace the "." separators with "/", and append the resource name
            string resourceNameAsPath = resourceNamespace.Replace(".", "/") + "/" + resourceName;

            Resource resource = new Resource(resourceAssembly, resourceNameAsPath);
            _publicIdResources.Add(publicId, resource);
            string assemblyName = resourceAssembly.GetName().Name.ToLower();
            if (!_assemblyMapping.ContainsKey(assemblyName))
            {
                _assemblyMapping.Add(assemblyName, resourceAssembly);
            }
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {

            if (absoluteUri.Scheme == ResourceScheme)
            {
                Assembly resourceAssembly = _assemblyMapping[absoluteUri.Host];
                string resourceName = absoluteUri.AbsolutePath.Substring(1).Replace("/", ".");
                Stream stream = resourceAssembly.GetManifestResourceStream(resourceName);
                return stream;
            }

            return base.GetEntity(absoluteUri, role, ofObjectToReturn);
        }

        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {

            // Is this a DocType Public Id?
            if (baseUri == null)
            {
                Resource resource;
                if (_publicIdResources.TryGetValue(relativeUri, out resource))
                {
                    return resource.AbsoluteUri;
                }
            }

            return base.ResolveUri(baseUri, relativeUri);
        }

        private const string ResourceScheme = "resource";
    }

    internal static class XhtmlParserContextFactory
    {

        internal static XmlParserContext CreateStrict()
        {
            XmlNameTable nameTable = new NameTable();
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
            XmlParserContext context = new XmlParserContext(nameTable, namespaceManager, null, XmlSpace.None);

            context.DocTypeName = "html";
            context.PublicId = "-//W3C//DTD XHTML 1.0 Strict//EN";
            context.SystemId = "xhtml1-strict.dtd";
            return context;
        }
    }

    internal static class XhtmlResolverFactory
    {

        internal static XmlResolver Create()
        {
            XmlResourceResolver resolver = new XmlResourceResolver();

            Assembly assembly = typeof(XhtmlResolverFactory).Assembly;
            string resourceNamespace = typeof(XhtmlResolverFactory).Namespace;

            resolver.AddPublicIdMapping("-//W3C//DTD XHTML 1.0 Strict//EN", assembly, resourceNamespace, XhtmlStrictDtd);
            resolver.AddPublicIdMapping("-//W3C//DTD XHTML 1.0 Transitional//EN", assembly, resourceNamespace, XhtmlTransitionalDtd);
            resolver.AddPublicIdMapping("-//W3C//DTD XHTML 1.0 Frameset//EN", assembly, resourceNamespace, XhtmlFramesetDtd);
            resolver.AddPublicIdMapping("xhtml-lat1.ent", assembly, resourceNamespace, XhtmlLat1Ent);
            resolver.AddPublicIdMapping("xhtml-symbol.ent", assembly, resourceNamespace, XhtmlSymbolEnt);
            resolver.AddPublicIdMapping("xhtml-special.ent", assembly, resourceNamespace, XhtmlSpecialEnt);

            return resolver;
        }

        private const string XhtmlStrictDtd = "xhtml1-strict.dtd";
        private const string XhtmlTransitionalDtd = "xhtml1-transitional.dtd";
        private const string XhtmlFramesetDtd = "xhtml1-frameset.dtd";
        private const string XhtmlLat1Ent = "xhtml-lat1.ent";
        private const string XhtmlSymbolEnt = "xhtml-symbol.ent";
        private const string XhtmlSpecialEnt = "xhtml-special.ent";
    }

}