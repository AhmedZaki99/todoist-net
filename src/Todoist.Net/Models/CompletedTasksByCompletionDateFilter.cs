using System;
using System.Collections.Generic;

using Todoist.Net.Extensions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a completed tasks query by completion date.
    /// </summary>
    public class CompletedTasksByCompletionDateFilter
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? Since { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? Until { get; set; }

        /// <summary>
        /// Gets or sets the workspace identifier.
        /// </summary>
        public string WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the section identifier.
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        /// Gets or sets the parent task identifier.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the filter query.
        /// </summary>
        public string FilterQuery { get; set; }

        /// <summary>
        /// Gets or sets the filter language.
        /// </summary>
        public string FilterLang { get; set; }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        public string PublicKey { get; set; }

        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            var parameters = new LinkedList<KeyValuePair<string, string>>();

            if (Since.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("since", Since.Value.ToFilterParameter()));
            }

            if (Until.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("until", Until.Value.ToFilterParameter()));
            }

            if (!string.IsNullOrEmpty(WorkspaceId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("workspace_id", WorkspaceId));
            }

            if (!string.IsNullOrEmpty(ProjectId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("project_id", ProjectId));
            }

            if (!string.IsNullOrEmpty(SectionId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("section_id", SectionId));
            }

            if (!string.IsNullOrEmpty(ParentId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("parent_id", ParentId));
            }

            if (!string.IsNullOrEmpty(FilterQuery))
            {
                parameters.AddLast(new KeyValuePair<string, string>("filter_query", FilterQuery));
            }

            if (!string.IsNullOrEmpty(FilterLang))
            {
                parameters.AddLast(new KeyValuePair<string, string>("filter_lang", FilterLang));
            }

            if (!string.IsNullOrEmpty(Cursor))
            {
                parameters.AddLast(new KeyValuePair<string, string>("cursor", Cursor));
            }

            if (Limit.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("limit", Limit.Value.ToString()));
            }

            if (!string.IsNullOrEmpty(PublicKey))
            {
                parameters.AddLast(new KeyValuePair<string, string>("public_key", PublicKey));
            }

            return parameters;
        }
    }
}
