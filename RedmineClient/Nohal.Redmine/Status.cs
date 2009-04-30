// <copyright file="Status.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-29</date>
// <summary>Class representing the issue status.</summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the issue status in Redmine
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets or sets the Id of the status
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the status
        /// </summary>
        public string Name { get; set; }
    }
}
