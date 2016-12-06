﻿using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents information about completed tasks.
    /// </summary>
    public class CompletedItemsInfo
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty("items")]
        public CompletedItem[] Items { get; internal set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        [JsonProperty("projects")]
        public IDictionary<int, Project> Projects { get; internal set; }
    }
}
