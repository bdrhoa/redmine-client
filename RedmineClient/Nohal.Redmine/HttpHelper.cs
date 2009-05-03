// <copyright file="HttpHelper.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-05-03</date>
// <summary>Class providing support for HTTP operations.</summary>
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class providing support for HTTP operations.
    /// </summary>
    internal class HttpHelper
    {
        /// <summary>
        /// Te container for assigned session cookie
        /// </summary>
        private CookieContainer cookieJar;

        /// <summary>
        /// Makes a web request
        /// </summary>
        /// <param name="request">Web request</param>
        /// <param name="method">Request method</param>
        /// <param name="postDataText">URLEncoded text for the post body</param>
        /// <returns>The response text</returns>
        private string WebRequest(HttpWebRequest request, string method, string postDataText)
        {
            if (this.cookieJar == null)
            {
                this.cookieJar = new CookieContainer();
            }
            request.CookieContainer = this.cookieJar;

            request.UserAgent = "Nohal.Redmine";
            //// set the connection keep-alive
            request.KeepAlive = true; // this is the default
            //// we don't want caching to take place so we need
            //// to set the pragma header to say we don't want caching
            request.Headers.Set("Pragma", "no-cache");
            //// set the request timeout to 5 min.
            request.Timeout = 300000;
            //// set the request method
            request.Method = method;

            //// add the content type so we can handle form data
            request.ContentType = "application/x-www-form-urlencoded";
            if (request.Method == "POST")
            {
                //// we need to store the data into a byte array
                byte[] postData = Encoding.ASCII.GetBytes(postDataText);
                request.ContentLength = postData.Length;
                Stream tempStream = request.GetRequestStream();
                //// write the data to be posted to the Request Stream
                tempStream.Write(postData, 0, postData.Length);
                tempStream.Close();
            }

            HttpWebResponse httpWResponse;
            try
            {
                httpWResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("The request to Redmine URL {0} caused the following exception: {1}.", request.Address, ex.Message));
            }

            if (this.cookieJar == null)
            {
                this.cookieJar = new CookieContainer();
                this.cookieJar.Add(httpWResponse.Cookies);
            }

            StreamReader sr = new StreamReader(httpWResponse.GetResponseStream(), Encoding.UTF8);
            string s = sr.ReadToEnd();
            httpWResponse.Close();
            return s;
        }

        /// <summary>
        /// Makes a web request using GET method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <returns>The response text</returns>
        internal string GetWebRequest(Uri requestUri)
        {
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "GET", String.Empty);
        }

        /// <summary>
        /// Makes a web request using POST method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <param name="postDataText">URLEncoded text for the post body</param>
        /// <returns>The response text</returns>
        internal string PostWebRequest(Uri requestUri, string postDataText)
        {
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "POST", postDataText);
        }

        /// <summary>
        /// Builds a query string from a collection of parameters
        /// </summary>
        /// <param name="parametersCollection">Collection of the parameters</param>
        /// <returns>The HTTP query string</returns>
        internal string CreateQueryString(NameValueCollection parametersCollection)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parametersCollection.Count; i++)
            {
                EncodeAndAddItem(ref sb, parametersCollection.GetKey(i), parametersCollection[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Encodes and adds a parameter to the query string
        /// </summary>
        /// <param name="sb">StringBuilder building query string</param>
        /// <param name="key">parameter name</param>
        /// <param name="value">parameter value</param>
        private void EncodeAndAddItem(ref StringBuilder sb, string key, string value)
        {
            if (sb.Length == 0)
            {
                String.Format("{0}={1}", key, HttpUtility.UrlEncode(value));  
            }
            else
            {
                String.Format("&{0}={1}", key, HttpUtility.UrlEncode(value));
            }
        }
    }
}
