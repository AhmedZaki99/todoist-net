using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a full project payload.
    /// </summary>
    public class ProjectFull
    {
        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonPropertyName("project")]
        public Project Project { get; internal set; }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        [JsonPropertyName("tasks")]
        public IReadOnlyCollection<DetailedTask> Tasks { get; internal set; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        [JsonPropertyName("comments")]
        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <value>The sections.</value>
        [JsonPropertyName("sections")]
        public IReadOnlyCollection<Section> Sections { get; internal set; }
    }
}
