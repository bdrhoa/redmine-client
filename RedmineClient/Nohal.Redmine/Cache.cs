// <copyright file="Cache.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-12-22</date>
// <summary>Cache for objects retrieved from the server.</summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace Nohal.Redmine
{
    internal class Cache
    {
        private DateTime _projectsRead;
        public TimeSpan InvalidationLifetime = new TimeSpan(0, 0, 20, 0);

        private List<Project> _projects;
        private Dictionary<int, List<Activity>> _activities = new Dictionary<int, List<Activity>>();
        private Dictionary<int, DateTime> _activitiesRead = new Dictionary<int, DateTime>();
        private Dictionary<int, List<Issue>> _issues = new Dictionary<int, List<Issue>>();
        private Dictionary<int, DateTime> _issuesRead = new Dictionary<int, DateTime>();

        internal List<Project> Projects
        {
            get
            {
                if (DateTime.Now.Subtract(_projectsRead) > InvalidationLifetime)
                {
                    _projects = null;
                }
                return _projects;
            }
            set
            {
                _projectsRead = DateTime.Now;
                _projects = value;
            } 
        }

        internal List<Activity> GetActivities(int projectId)
        {
            DateTime read;
            _activitiesRead.TryGetValue(projectId, out read);
            if (read == DateTime.MinValue || DateTime.Now.Subtract(read) > InvalidationLifetime)
            {
                return null;
            }

            List<Activity> lst;
            _activities.TryGetValue(projectId, out lst);
            return lst;
        }

        internal void SetActivities(int projectId, List<Activity> activities)
        {
            _activitiesRead[projectId] = DateTime.Now;
            _activities[projectId] = activities;
        }

        internal List<Issue> GetIssues(int projectId)
        {
            DateTime read;
            _issuesRead.TryGetValue(projectId, out read);
            if (read == DateTime.MinValue || DateTime.Now.Subtract(read) > InvalidationLifetime)
            {
                return null;
            }

            List<Issue> lst;
            _issues.TryGetValue(projectId, out lst);
            return lst;
        }

        internal void SetIssues(int projectId, List<Issue> issues)
        {
            _issuesRead[projectId] = DateTime.Now;
            _issues[projectId] = issues;
        }

        internal void InvalidateCache()
        {
            _projects = null;
            _activities = new Dictionary<int, List<Activity>>();
            _activitiesRead = new Dictionary<int, DateTime>();
            _issues = new Dictionary<int, List<Issue>>();
            _issuesRead = new Dictionary<int, DateTime>();
        }
    }

    public class CachedObject
    {
        internal DateTime Created;
        internal TimeSpan Age
        {
            get
            {
                return DateTime.Now.Subtract(Created);
            }
        }

        public CachedObject()
        {
            Created = DateTime.Now;
        }
    }
}
