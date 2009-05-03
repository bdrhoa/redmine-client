// <copyright file="Issue.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Class representing the issue in Redmine.</summary>
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the issue in Redmine
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Gets or sets the issue Id in Redmine
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the issue subject text
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets ID of the issue tracker
        /// </summary>
        public int TrackerId { get; set; }

        /// <summary>
        /// Gets or sets Description of the issue
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets ID of the issue status
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets ID of the issue priority
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        /// Gets or sets ID of the assigned user
        /// </summary>
        public int AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets Start date of the issue
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets Due date of the issue
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets Estimated time for completing the issue
        /// </summary>
        public double EstimatedTime { get; set; }

        /// <summary>
        /// Gets or sets Percentual value of the issue completion
        /// </summary>
        public int PercentDone { get; set; }

        /// <summary>
        /// Gets or sets ID of the version the issue targets
        /// </summary>
        public int TargetVersionId { get; set; }

        /// <summary>
        /// List of related issues
        /// </summary>
        public Dictionary<int, IssueRelationType> RelatedIssues;

        /// <summary>
        /// Users watching this issue
        /// </summary>
        public List<User> Watchers;

        /// <summary>
        /// Additional issue parameters.
        /// This property can be used for specifying additional parameters specific for your particular Redmine instance.
        /// </summary>
        public NameValueCollection AdditionalParameters { get; set; }

        // TODO: File attachments
    }
}
