// <copyright file="IssueRelationType.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-05-03</date>
// <summary>Class representing the type of issue relation.</summary>

namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the type of issue relation.
    /// </summary>
    public class IssueRelationType
    {
        /// <summary>
        /// Gets the value for the issue which is related to another issue
        /// </summary>
        public static string RelatedTo
        {
            get { return "relatedto"; }
        }

            /// <summary>
        /// Gets the value for the issue which is a duplicate of another issue
        /// </summary>
        public static string Duplicates
        {
            get { return "duplicates"; }
        }
        
        /// <summary>
        /// Gets the value for the issue which blocks another issue
        /// </summary>
        public static string Blocks
        {
            get { return "blocks"; }
        }
        
        /// <summary>
        /// Gets the value for the issue which precedes another issue
        /// </summary>
        public static string Precedes
        {
            get { return "precedes"; }
        }
    }
}
