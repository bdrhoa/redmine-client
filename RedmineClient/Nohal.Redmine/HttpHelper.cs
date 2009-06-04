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
    /// Helper class for constructing the multipart/form-data POST requests
    /// </summary>
    public class MultipartData
    {
        /// <summary>
        /// Boundary dividing form fields
        /// </summary>
        private static string boundary = "-----------------------------7d951471538";
        
        /// <summary>
        /// Request data
        /// </summary>
        private byte[] data = new byte[0];

        /// <summary>
        /// Is the request closed?
        /// </summary>
        private bool finished = false;

        /// <summary>
        /// Gets the Boundary string
        /// </summary>
        public static string Boundary
        {
            get
            {
                return boundary;
            }
        }

        /// <summary>
        /// Gets the data of the request
        /// </summary>
        public byte[] Data
        {
            get
            {
                this.Finish();
                return this.data;
            }
        }

        /// <summary>
        /// Adds new form field name/value pair
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">Tha value of the field</param>
        public void AddValue(string name, string value)
        {
            if (this.finished)
            {
                throw new HttpRequestValidationException("The request is finished, you can't add any more fields.");
            }

            string text;
            text = "--" + boundary + "\r\n";
            text += "Content-Disposition: form-data; name=\"" + name + "\"\r\n\r\n";
            text += value + "\r\n";

            byte[] bytes = Encoding.ASCII.GetBytes(text);

            byte[] final = new byte[this.data.Length + bytes.Length];
            Buffer.BlockCopy(this.data, 0, final, 0, this.data.Length);
            Buffer.BlockCopy(bytes, 0, final, this.data.Length, bytes.Length);
            this.data = final;
        }

        /// <summary>
        /// Adds a file to the post request
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="fileName">The file path</param>
        public void AddFile(string name, string fileName)
        {
            if (this.finished)
            {
                throw new HttpRequestValidationException("The request is finished, you can't add any more files.");
            }

            string text;
            text = "--" + boundary + "\r\n";
            text += "Content-Disposition: form-data; name=\"" + name + "\"; filename=\"" + fileName + "\"\r\n";
            text += "Content-Type: application/octet-stream\r\n\r\n";

            byte[] textBytes = Encoding.ASCII.GetBytes(text);
            byte[] fileBytes;

            try
            {
                fileBytes = File.ReadAllBytes(fileName);
            }
            catch (Exception)
            {
                fileBytes = new byte[0];
            }

            byte[] final = new byte[this.data.Length + textBytes.Length + fileBytes.Length + 2];
            Buffer.BlockCopy(this.data, 0, final, 0, this.data.Length);
            Buffer.BlockCopy(textBytes, 0, final, this.data.Length, textBytes.Length);
            Buffer.BlockCopy(fileBytes, 0, final, this.data.Length + textBytes.Length, fileBytes.Length);
            final[final.Length - 2] = 13;
            final[final.Length - 1] = 10;
            this.data = final;
        }

        /// <summary>
        /// Finishes the creation of the request
        /// </summary>
        private void Finish()
        {
            if (!this.finished)
            {
                this.finished = true;
                byte[] bytes = Encoding.ASCII.GetBytes("--" + boundary + "--");

                byte[] final = new byte[this.data.Length + bytes.Length];
                Buffer.BlockCopy(this.data, 0, final, 0, this.data.Length);
                Buffer.BlockCopy(bytes, 0, final, this.data.Length, bytes.Length);
                this.data = final;
            }
        }
    }

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
        /// Makes a web request using GET method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <returns>The response text</returns>
        internal string GetWebRequest(Uri requestUri)
        {
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "GET", null);
        }

        /// <summary>
        /// Makes a web request using POST method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <param name="postDataText">URLEncoded text for the post body</param>
        /// <returns>The response text</returns>
        internal string PostUrlEncodedWebRequest(Uri requestUri, string postDataText)
        {
            //// we need to store the data into a byte array
            byte[] postData = Encoding.ASCII.GetBytes(postDataText);
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "POST", postData);
        }

        /// <summary>
        /// Makes a multipart/form-data web request using POST method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <param name="postData">The data to POST to the server</param>
        /// <returns>The response text</returns>
        internal string PostMultipartFormDataWebRequest(Uri requestUri, byte[] postData)
        {
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "POSTmultipart", postData);
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
                this.EncodeAndAddItem(ref sb, parametersCollection.GetKey(i), parametersCollection[i]);
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

        /// <summary>
        /// Makes a web request
        /// </summary>
        /// <param name="request">Web request</param>
        /// <param name="method">Request method</param>
        /// <param name="postData">Data for the post body</param>
        /// <returns>The response text</returns>
        private string WebRequest(HttpWebRequest request, string method, byte[] postData)
        {
            System.Net.ServicePointManager.Expect100Continue = false; // Fix for: The remote server returned an error: (417) Expectation Failed.
            if (this.cookieJar == null)
            {
                this.cookieJar = new CookieContainer();
            }

            //request.Proxy = new WebProxy("http://localhost:8888");
            request.CookieContainer = this.cookieJar;

            request.UserAgent = "Nohal.Redmine";
            //// set the connection keep-alive
            request.KeepAlive = true; // this is the default
            //// we don't want caching to take place so we need
            //// to set the pragma header to say we don't want caching
            request.Headers.Set("Pragma", "no-cache");
            //// set the request timeout to 5 min.
            request.Timeout = 300000;

            //// add the content type so we can handle form data
            if (method == "GET" || method == "POST")
            {
                //// set the request method
                request.Method = method;
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary=" + MultipartData.Boundary;
            }

            if (request.Method == "POST")
            {
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
    }
}
