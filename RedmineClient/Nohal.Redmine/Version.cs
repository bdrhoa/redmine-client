// <copyright file="Version.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-05-03</date>
// <summary>Class representing the version of the project.</summary>

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the version of the project.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Gets or sets the version Id in Redmine
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the version name
        /// </summary>
        public string Name { get; set; }
    }
}
