// <copyright file="Project.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Class representing the project in Redmine.</summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the project in Redmine
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the Id of the project
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project
        /// </summary>
        public string Name { get; set; }
    }
}
