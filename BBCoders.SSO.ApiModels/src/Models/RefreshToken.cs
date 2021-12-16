using System;
using System.Collections.Generic;

namespace BBCoders.SSO.ApiModels
{
    /// <summary>
    /// Models a refresh token.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the life time.
        /// </summary>
        /// <value>
        /// The life time.
        /// </value>
        public int Lifetime { get; set; }

        /// <summary>
        /// Gets or sets the consumed time.
        /// </summary>
        /// <value>
        /// The consumed time.
        /// </value>
        public DateTime? ConsumedTime { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public Token AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; } = 4;
    }
}