using System;

namespace SimpleChatApp.Models
{
    /// <summary>
    /// Model for User status
    /// </summary>
    public class StatusModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is idle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is idle; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdle { get; set; } = true;
    }
}
