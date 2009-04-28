// <copyright file="Activity.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Class representing the activity performed while working on an issue.</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the activity in Redmine
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Gets or sets the Id of the activity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the activity
        /// </summary>
        public string Description { get; set; }
    }
}
