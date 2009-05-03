﻿// <copyright file="Redmine.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Contains a wrapper for the Redmine project management system.</summary>
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Web;
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
        private const string ProjectListRelativeUri = "projects?format=atom";

        /// <summary>
        /// Relative path to the login form
        /// </summary>
        private const string LoginRelativeUri = "login";

        /// <summary>
        /// Relative path to the list of all the project issues
        /// </summary>
        private const string IssueListRelativeUri = "projects/{0}/issues?format=atom&per_page=999999";

        /// <summary>
        /// Relative path to the project settings Information form
        /// </summary>
        private const string ProjectSettingsInfoUri = "projects/settings/{0}";

        /// <summary>
        /// Relative path to the new issue form
        /// </summary>
        private const string NewIssueRelativeUri = "projects/{0}/issues/new";
        
        /// <summary>
        /// Relative path to the time logging form
        /// </summary>
        private const string TimeLogFormRelativeUri = "projects/{0}/timelog/edit";

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
        /// Http Helper class
        /// </summary>
        private HttpHelper httpHelper;

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
                this.redmineBaseUri = new Uri(value, UriKind.Absolute);
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
        /// Initializes a new instance of the Redmine class.
        /// </summary>
        public Redmine()
        {
            this.httpHelper = new HttpHelper();
        }

        /// <summary>
        /// Logs user into Redmine
        /// </summary>
        public void LogIn()
        {
            if (!string.IsNullOrEmpty(RedmineUser))
            {
                string requestData = String.Format(LoginRequest,
                                                   System.Web.HttpUtility.UrlEncode(
                                                       this.ConstructUri(ProjectListRelativeUri).ToString()),
                                                   System.Web.HttpUtility.UrlEncode(this.RedmineUser),
                                                   System.Web.HttpUtility.UrlEncode(this.RedminePassword));
                XhtmlPage page = new XhtmlPage(this.httpHelper.PostWebRequest(this.ConstructUri(LoginRelativeUri), requestData));

                // if we get a feed with projects, we assume that we are successfully authenticated
                // if we do get xhtml, it's the login form again
                // this solution is quite ugly, but redmine doesn't provide much help in knowing what went wrong anyway...
                if (page.XmlDocument.DocumentElement != null && page.XmlDocument.DocumentElement.Name == "feed")
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
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(ProjectListRelativeUri)));
            List<Project> projects = new List<Project>();
            foreach (AtomEntry entry in AtomParser.ParseFeed(page.XmlDocument))
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
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(IssueListRelativeUri, projectId))));
            List<Issue> issues = new List<Issue>();
            foreach (AtomEntry entry in AtomParser.ParseFeed(page.XmlDocument))
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
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(TimeLogFormRelativeUri, projectId))));

            List<Activity> activities = new List<Activity>();
            foreach (KeyValuePair<int, string> kv in page.GetSelectOptions("time_entry_activity_id"))
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
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(NewIssueRelativeUri, projectId))));
            List<Tracker> trackers = new List<Tracker>();
            foreach (KeyValuePair<int, string> kv in page.GetSelectOptions("issue_tracker_id"))
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
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(NewIssueRelativeUri, projectId))));
            List<Status> statuses = new List<Status>();
            foreach (KeyValuePair<int, string> kv in page.GetSelectOptions("issue_status_id"))
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
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(NewIssueRelativeUri, projectId))));
            List<Priority> priorities = new List<Priority>();
            foreach (KeyValuePair<int, string> kv in page.GetSelectOptions("issue_priority_id"))
            {
                priorities.Add(new Priority() { Id = kv.Key, Name = kv.Value });
            }

            return priorities;
        }

        /// <summary>
        /// Gets the list of all versions project issue can target
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the versions available for the user in selected project</returns>
        public List<Version> GetVersions(int projectId)
        {
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(NewIssueRelativeUri, projectId))));
            List<Version> versions = new List<Version>();
            foreach (KeyValuePair<int, string> kv in page.GetSelectOptions("issue_fixed_version_id"))
            {
                versions.Add(new Version() { Id = kv.Key, Name = kv.Value });
            }

            return versions;
        }

        /// <summary>
        /// Gets the list of all the users available as watchers for the project
        /// </summary>
        /// <param name="projectId">ID of the project</param>
        /// <returns>List of all the users available as watchers for the project</returns>
        public List<User> GetWatchers(int projectId)
        {
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(NewIssueRelativeUri, projectId))));
            List<User> watchers = new List<User>();
            foreach (KeyValuePair<int, string> kv in page.GetCheckBoxOptions("issue[watcher_user_ids][]"))
            {
                watchers.Add(new User() { Id = kv.Key, Name = kv.Value });
            }

            return watchers;
        }

        /// <summary>
        /// Gets the list of all users available to be assigned to project issue
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>List of all the users available to be assigned to project issues</returns>
        public List<User> GetAsignees(int projectId)
        {
            XhtmlPage page = new XhtmlPage(this.httpHelper.GetWebRequest(this.ConstructUri(String.Format(NewIssueRelativeUri, projectId))));
            List<User> users = new List<User>();
            foreach (KeyValuePair<int, string> kv in page.GetSelectOptions("issue_assigned_to_id"))
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
            string requestData = String.Format(TimeLogRequest,
                                               System.Web.HttpUtility.UrlEncode(
                                                   new Uri(redmineBaseUri, TimeLogFormRelativeUri).ToString()),
                                               System.Web.HttpUtility.UrlEncode(issueId.ToString()),
                                               System.Web.HttpUtility.UrlEncode(date.ToString("yyyy-MM-dd")),
                                               System.Web.HttpUtility.UrlEncode(String.Format("{0:0.##}", timeSpent)),
                                               System.Web.HttpUtility.UrlEncode(description),
                                               System.Web.HttpUtility.UrlEncode(activityId.ToString()));
            this.httpHelper.PostWebRequest(this.ConstructUri(String.Format(TimeLogFormRelativeUri, projectId)), requestData);
        }

        /// <summary>
        /// Creates new issue in Redmine
        /// </summary>
        /// <param name="newIssue">Issue to create in Redmine</param>
        public void CreateIssue(Issue newIssue)
        {
            NameValueCollection requestParameters = new NameValueCollection();
            foreach(User user in newIssue.Watchers)
            {
                requestParameters.Add("issue[watcher_user_ids][]", user.Id.ToString());
            }
        }

        /// <summary>
        /// Constructs the complete URL and tries to eliminate all the possible causes for Uri to fail.
        /// </summary>
        /// <param name="redmineRelativeUri">relative Uri of the Redmine object</param>
        /// <returns>Uri of the redmine page</returns>
        private Uri ConstructUri(string redmineRelativeUri)
        {
            string relativepath;
            relativepath = VirtualPathUtility.AppendTrailingSlash(redmineBaseUri.AbsolutePath);
            UriBuilder ub = new UriBuilder(this.redmineBaseUri.Scheme, redmineBaseUri.Host, redmineBaseUri.Port, relativepath);
            return new Uri(ub.Uri + redmineRelativeUri);
        }
    }
}
