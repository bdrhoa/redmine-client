// <copyright file="AtomEntry.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-27</date>
// <summary>Class representing the Atom feed entry.</summary>
using System;

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
        /// Gets or sets the Content of the entry
        /// </summary>
        internal string Content { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the entry
        /// </summary>
        internal string Updated { get; set; }

        /// <summary>
        /// Gets or sets the author of the entry
        /// </summary>
        internal AtomAuthor Author { get; set; }

        /// <summary>
        /// Gets the numeric Id of the entry from the last number contained in the entry address
        /// </summary>
        internal int NumericId
        {
            get
            {
                return Convert.ToInt32(this.Id.Substring(this.Id.LastIndexOf('/') + 1));
            }
        }

        /// <summary>
        /// Class representing the author of the atom entry
        /// </summary>
        internal class AtomAuthor
        {
            /// <summary>
            /// Gets or sets the name of the author
            /// </summary>
            internal string Name { get; set; }

            /// <summary>
            /// Gets or sets the e-mail address of the author
            /// </summary>
            internal string Email { get; set; }
        }
    }
}
