// <copyright file="Redmine.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Contains a wrapper for the Redmine project management system.</summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Xml;

namespace Nohal.Redmine
{
    /// <summary>
    /// Wrapper class for the Redmine project management system
    /// </summary>
    public class Redmine
    {
        /// <summary>
        /// Relative path to the project list
        /// </summary>
        private const string ProjectListRelativeUri = "/projects?format=atom";

        /// <summary>
        /// Relative path to the login form
        /// </summary>
        private const string LoginRelativeUri = "/login";

        /// <summary>
        /// Relative path to the list of all the project issues
        /// </summary>
        private const string IssueListRelativeUri = "/projects/{0}/issues?format=atom&per_page=999999";

        /// <summary>
        /// Relative path to the project settings Information form
        /// </summary>
        private const string ProjectSettingsInfoUri = "/projects/settings/{0}";

        /// <summary>
        /// Relative path to the new issue form
        /// </summary>
        private const string NewIssueRelativeUri = "/projects/{0}/issues/new";
        
        /// <summary>
        /// Relative path to the time logging form
        /// </summary>
        private const string TimeLogFormRelativeUri = "/projects/{0}/timelog/edit";

        /// <summary>
        /// Form request for login
        /// </summary>
        private const string LoginRequest =
            "back_url={0}&username={1}&password={2}&login=Login";

        /// <summary>
        /// Form request for time log
        /// </summary>
        private const string TimeLogRequest =
            "back_url={0}&time_entry[issue_id]={1}&time_entry[spent_on]={2}&time_entry[hours]={3}&time_entry[comments]={4}&time_entry[activity_id]={5}&commit=Save";

        /// <summary>
        /// Base URI of the Redmine installation
        /// </summary>
        private Uri redmineBaseUri;

        /// <summary>
        /// Username used to connect to the Redmine installation
        /// </summary>
        private string redmineUser;

        /// <summary>
        /// Password used to connect to the Redmine installation
        /// </summary>
        private string redminePassword;

        /// <summary>
        /// Is the user already successfully authenticated?
        /// </summary>
        private bool authenticated = false;

        /// <summary>
        /// Te container for assigner session cookie
        /// </summary>
        private CookieContainer cookieJar;

        /// <summary>
        /// Gets or sets the base URI of the Redmine installation
        /// </summary>
        public string RedmineBaseUri
        {
            get
            {
                return this.redmineBaseUri.ToString();
            }

            set
            {
                this.redmineBaseUri = new Uri(value);
            }
        }

        /// <summary>
        /// Gets or sets the Username used when connecting to Redmine
        /// </summary>
        public string RedmineUser
        {
            get
            {
                return this.redmineUser;
            }

            set
            {
                this.redmineUser = value;
            }
        }

        /// <summary>
        /// Gets or sets the Password used when connecting to Redmine
        /// </summary>
        public string RedminePassword
        {
            get
            {
                return this.redminePassword;
            }

            set
            {
                this.redminePassword = value;
            }
        }

        /// <summary>
        /// Logs user into Redmine
        /// </summary>
        public void LogIn()
        {
            if (!string.IsNullOrEmpty(RedmineUser))
            {
                Uri uri = new Uri(this.redmineBaseUri, LoginRelativeUri);
                string requestData = String.Format(LoginRequest,
                                                   System.Web.HttpUtility.UrlEncode(
                                                       new Uri(redmineBaseUri, ProjectListRelativeUri).ToString()),
                                                   System.Web.HttpUtility.UrlEncode(this.RedmineUser),
                                                   System.Web.HttpUtility.UrlEncode(this.RedminePassword));
                string s = this.PostWebRequest(uri, requestData);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(s);

                // if we get a feed with projects, we assume that we are successfully authenticated
                // if we do get xhtml, it's the login form again
                // this solution is quite ugly, but redmine doesn't provide much help in knowing what went wrong anyway...
                if (xml.DocumentElement != null && xml.DocumentElement.Name == "feed")
                {
                    this.authenticated = true;
                }
                else
                {
                    throw new AuthenticationException("Authentication in Redmine failed.");
                }
            }
        }

        /// <summary>
        /// Logs user into Redmine
        /// </summary>
        /// <param name="username">Username in Redmine</param>
        /// <param name="password">Password in Redmine</param>
        public void LogIn(string username, string password)
        {
            this.RedmineUser = username;
            this.RedminePassword = password;
            this.LogIn();
        }

        /// <summary>
        /// Gets the list of all the available projects
        /// </summary>
        /// <returns>List of all the projects available to the user</returns>
        public List<Project> GetProjects()
        {
            Uri uri = new Uri(this.redmineBaseUri, ProjectListRelativeUri);
            string s = this.GetWebRequest(uri);
            List<Project> projects = new List<Project>();
            foreach (AtomEntry entry in AtomParser.ParseFeed(s))
            {
                projects.Add(new Project
                                 {
                                     Id = entry.NumericId,
                                     Name = entry.Title
                                 });
            }

            return projects;
        }

        /// <summary>
        /// Gets the list of all the available issues
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the available issues for the project</returns>
        public List<Issue> GetIssues(int projectId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(IssueListRelativeUri, projectId));
            string s = this.GetWebRequest(uri);
            List<Issue> issues = new List<Issue>();
            foreach (AtomEntry entry in AtomParser.ParseFeed(s))
            {
                issues.Add(new Issue
                {
                    Id = entry.NumericId,
                    Subject = entry.Title
                });
            }

            return issues;
        }

        /// <summary>
        /// Gets the list of all available project activities
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the activities available for the user</returns>
        public List<Activity> GetActivities(int projectId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(TimeLogFormRelativeUri, projectId));
            string s = this.GetWebRequest(uri);
            XmlDocument docx = new XmlDocument();
            docx.LoadXml(s);
            List<Activity> activities = new List<Activity>();
            foreach (KeyValuePair<int, string> kv in ParseSelect(docx.GetElementById("time_entry_activity_id")))
            {
                activities.Add(new Activity() { Id = kv.Key, Description = kv.Value });
            }

            return activities;
        }

        /// <summary>
        /// Gets the list of all available project trackers
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the trackers available for the user in selected project</returns>
        public List<Tracker> GetTrackers(int projectId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(NewIssueRelativeUri, projectId));
            string s = this.GetWebRequest(uri);
            XmlDocument docx = new XmlDocument();
            docx.LoadXml(s);
            List<Tracker> trackers = new List<Tracker>();
            foreach (KeyValuePair<int, string> kv in ParseSelect(docx.GetElementById("issue_tracker_id")))
            {
                trackers.Add(new Tracker() {Id = kv.Key, Name = kv.Value});
            }

            return trackers;
        }

        /// <summary>
        /// Gets the list of all available project issue statuses
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the issue statuses available for the user in selected project</returns>
        public List<Status> GetStatuses(int projectId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(NewIssueRelativeUri, projectId));
            string s = this.GetWebRequest(uri);
            XmlDocument docx = new XmlDocument();
            docx.LoadXml(s);
            List<Status> statuses = new List<Status>();
            foreach (KeyValuePair<int, string> kv in ParseSelect(docx.GetElementById("issue_status_id")))
            {
                statuses.Add(new Status() { Id = kv.Key, Name = kv.Value });
            }

            return statuses;
        }

        /// <summary>
        /// Gets the list of all available project issue priorities
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the issue priorities available for the user in selected project</returns>
        public List<Priority> GetPriorities(int projectId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(NewIssueRelativeUri, projectId));
            string s = this.GetWebRequest(uri);
            XmlDocument docx = new XmlDocument();
            docx.LoadXml(s);
            List<Priority> priorities = new List<Priority>();
            foreach (KeyValuePair<int, string> kv in ParseSelect(docx.GetElementById("issue_priority_id")))
            {
                priorities.Add(new Priority() { Id = kv.Key, Name = kv.Value });
            }

            return priorities;
        }

        /// <summary>
        /// Gets the list of all users available to be assigned to project issue
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the users available to be assigned to project issues</returns>
        public List<User> GetAsignees(int projectId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(NewIssueRelativeUri, projectId));
            string s = this.GetWebRequest(uri);
            XmlDocument docx = new XmlDocument();
            docx.LoadXml(s);
            List<User> users = new List<User>();
            foreach (KeyValuePair<int, string> kv in ParseSelect(docx.GetElementById("issue_assigned_to_id")))
            {
                users.Add(new User() { Id = kv.Key, Name = kv.Value });
            }

            return users;
        }

        /// <summary>
        /// Logs time spent on an issue
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="issueId">Issue Id</param>
        /// <param name="description">Work description</param>
        /// <param name="timeSpent">Time spent on the issue</param>
        /// <param name="date">Date when the work was done</param>
        /// <param name="activityId">Activity Id</param>
        public void LogTimeForIssue(int projectId, int issueId, string description, double timeSpent, DateTime date, int activityId)
        {
            Uri uri = new Uri(this.redmineBaseUri, String.Format(TimeLogFormRelativeUri, projectId));
            string requestData = String.Format(TimeLogRequest,
                                               System.Web.HttpUtility.UrlEncode(
                                                   new Uri(redmineBaseUri, TimeLogFormRelativeUri).ToString()),
                                               System.Web.HttpUtility.UrlEncode(issueId.ToString()),
                                               System.Web.HttpUtility.UrlEncode(date.ToString("yyyy-MM-dd")),
                                               System.Web.HttpUtility.UrlEncode(String.Format("{0:0.##}", timeSpent)),
                                               System.Web.HttpUtility.UrlEncode(description),
                                               System.Web.HttpUtility.UrlEncode(activityId.ToString()));
            this.PostWebRequest(uri, requestData);
        }

        /// <summary>
        /// Makes a web request
        /// </summary>
        /// <param name="request">Web request</param>
        /// <param name="method">Request method</param>
        /// <param name="postDataText">URLEncoded text for the post body</param>
        /// <returns>The response text (HTML or XML)</returns>
        private string WebRequest(HttpWebRequest request, string method, string postDataText)
        {
            if (!this.authenticated)
            {
                this.cookieJar = new CookieContainer();
            }

            if (this.cookieJar != null)
            {
                request.CookieContainer = this.cookieJar;
            }

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

            HttpWebResponse httpWResponse = (HttpWebResponse)request.GetResponse();
            //// Read the raw response from the request
            StreamReader sr = new StreamReader(httpWResponse.GetResponseStream(), Encoding.ASCII);
            //// Convert the stream to a string
            string s = sr.ReadToEnd();
            sr.Close();
            if (this.cookieJar == null)
            {
                this.cookieJar = new CookieContainer();
                this.cookieJar.Add(httpWResponse.Cookies);
            }

            httpWResponse.Close();
            return s;
        }

        /// <summary>
        /// Makes a web request using GET method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <returns>The response text (HTML or XML)</returns>
        private string GetWebRequest(Uri requestUri)
        {
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "GET", String.Empty);
        }

        /// <summary>
        /// Makes a web request using POST method
        /// </summary>
        /// <param name="requestUri">Requested address</param>
        /// <param name="postDataText">URLEncoded text for the post body</param>
        /// <returns>The response text (HTML or XML)</returns>
        private string PostWebRequest(Uri requestUri, string postDataText)
        {
            return this.WebRequest((HttpWebRequest)System.Net.WebRequest.Create(requestUri), "POST", postDataText);
        }

        /// <summary>
        /// Parses XHTML combobox possible values
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
