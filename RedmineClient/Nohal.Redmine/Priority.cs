// <copyright file="Priority.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-29</date>
// <summary>Class representing the issue priority.</summary>
namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the issue priority in Redmine
    /// </summary>
    public class Priority
    {
        /// <summary>
        /// Gets or sets the Id of the priority
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the priority
        /// </summary>
        public string Name { get; set; }
    }
}
