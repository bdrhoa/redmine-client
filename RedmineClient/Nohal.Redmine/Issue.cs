// <copyright file="Issue.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Class representing the issue in Redmine.</summary>
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
    }
}
