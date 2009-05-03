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
        /// Issue is related to another issue
        /// </summary>
        public static string RelatedTo = "relatedto";

        /// <summary>
        /// Issue is a duplicate of another issue
        /// </summary>
        public static string Duplicates = "duplicates";
        
        /// <summary>
        /// Issue blocks another issue
        /// </summary>
        public static string Blocks = "blocks";
        
        /// <summary>
        /// Issue precedes another issue
        /// </summary>
        public static string Precedes = "precedes";
    }
}
