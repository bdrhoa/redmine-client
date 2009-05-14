// <copyright file="RecentActivityType.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-05-14</date>
// <summary>Enumerator representing the activity type.</summary>

namespace Nohal.Redmine
{
    /// <summary>
    /// Enumerator representing the activity type.
    /// </summary>
    public enum RecentActivityType : byte 
    {
        /// <summary>
        /// Changes in the code base
        /// </summary>
        Changesets = 1,

        /// <summary>
        /// Added documents
        /// </summary>
        Documents = 2,
        
        /// <summary>
        /// Added files
        /// </summary>
        Files = 3,
        
        /// <summary>
        /// Issue activities
        /// </summary>
        Issues = 4,
        
        /// <summary>
        /// Forum activities
        /// </summary>
        Messages = 5,
        
        /// <summary>
        /// Project news
        /// </summary>
        News = 6,
        
        /// <summary>
        /// Activities in the project wiki
        /// </summary>
        WikiEdits = 7
    }
}
