// <copyright file="ProjectActivity.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-05-14</date>
// <summary>Class representing the recent project activity.</summary>

using System;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the recent project activity.
    /// </summary>
    public class ProjectActivity
    {
        /// <summary>
        /// Gets or sets the title of the activity
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Url of the activity
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Gets or sets the timestamp of the activity
        /// </summary>
        public DateTime Updated { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the activity
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the author
        /// </summary>
        public string AuthorName { get; set; }
        
        /// <summary>
        /// Gets or sets the e-mail address of the author
        /// </summary>
        public string AuthorEmail { get; set; }
    }
}
