// <copyright file="AtomEntry.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Class representing the Atom feed entry.</summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing an entry of the atom feed
    /// </summary>
    internal class AtomEntry
    {
        /// <summary>
        /// Gets or sets the title of the entry
        /// </summary>
        internal string Title { get; set; }

        /// <summary>
        /// Gets or sets the Id of the entry
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        /// Gets the numeric Id of the entry from the last number contained in the entry address
        /// </summary>
        internal int NumericId
        {
            get
            {
                return Convert.ToInt32(Id.Substring(Id.LastIndexOf('/') + 1));
            }
        }
    }
}
