// <copyright file="User.cs" company="Pavel Kalian">
// Copyright (c) 2009 All Right Reserved
// </copyright>
// <author>Pavel Kalian</author>
// <email>pavel@kalian.cz</email>
// <date>2009-04-29</date>
// <summary>Class representing the user.</summary>
namespace Nohal.Redmine
{
    /// <summary>
    /// Class representing the user in Redmine
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the Id of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user
        /// </summary>
        public string Name { get; set; }
    }
}
