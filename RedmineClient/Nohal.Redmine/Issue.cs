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
    /// Public interface for the issue list
    /// </summary>
    public interface IIssue
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the issue.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject of the issue.</value>
        string Subject { get; set; }
    }

    /// <summary>
    /// Class representing the issue in Redmine
    /// </summary>
    public class Issue : IIssue
    {
        /// <summary>
        /// Gets or sets the list of related issues
        /// </summary>
        public Dictionary<int, IssueRelationType> RelatedIssues { get; set; }

        /// <summary>
        /// Gets or sets the users watching this issue
        /// </summary>
        public List<User> Watchers { get; set; }

        /// <summary>
        /// Gets or sets the file attachments (Path => Description)
        /// </summary>
        public NameValueCollection Attachments { get; set; }

        /// <summary>
        /// Gets or sets the issue Id in Redmine
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Id of the project
        /// </summary>
        public int ProjectId { get; set; }

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
        /// Gets or sets additional issue parameters.
        /// This property can be used for specifying additional parameters specific for your particular Redmine instance.
        /// </summary>
        public NameValueCollection AdditionalParameters { get; set; }

        /// <summary>
        /// Prepares the request data
        /// </summary>
        /// <returns>The request data</returns>
        internal byte[] MakeRequestData()
        {
            MultipartData data = new MultipartData();
            data.AddValue("authenticity_token", Redmine.AuthenticityToken);
            data.AddValue("issue[tracker_id]", this.TrackerId.ToString());
            data.AddValue("issue[subject]", this.Subject);
            data.AddValue("issue[description]", this.Description);
            data.AddValue("issue[status_id]", this.StatusId.ToString());
            data.AddValue("issue[priority_id]", this.PriorityId.ToString());
            data.AddValue("issue[assigned_to_id]", this.AssignedTo.ToString());
            data.AddValue("issue[fixed_version_id]", this.TargetVersionId.ToString());
            if (this.Start != DateTime.MinValue)
            {
                data.AddValue("issue[start_date]", this.Start.ToString("yyyy-MM-dd"));
            }

            if (this.DueDate != DateTime.MinValue)
            {
                data.AddValue("issue[due_date]", this.DueDate.ToString("yyyy-MM-dd"));   
            }

            data.AddValue("issue[estimated_hours]", this.EstimatedTime.ToString());
            data.AddValue("issue[done_ratio]", this.PercentDone.ToString());
            if (this.Attachments != null)
            {
                int counter = 1;
                foreach (KeyValuePair<string, string> attachment in this.Attachments)
                {
                    data.AddFile(String.Format("attachments[{0}][file]", counter), attachment.Key);
                    data.AddValue(String.Format("attachments[{0}][description]", counter), attachment.Value);
                    counter++;
                }
            }

            if (this.Watchers != null)
            {
                foreach (User watcher in this.Watchers)
                {
                    data.AddValue("issue[watcher_user_ids][]", watcher.Id.ToString());
                }
            }

            if (this.AdditionalParameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in this.AdditionalParameters)
                {
                    data.AddValue(parameter.Key, parameter.Value);
                }
            }

            data.AddValue("commit", "Create");
            return data.Data;
        }
    }
}
