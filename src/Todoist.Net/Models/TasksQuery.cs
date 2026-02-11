using System.Collections.Generic;
using System.Linq;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a tasks list query.
    /// </summary>
    public class TasksQuery
    {
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
        /// Gets or sets the label name.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the task identifiers.
        /// </summary>
        public IEnumerable<string> Ids { get; set; }

        /// <summary>
        /// Gets or sets the pagination cursor.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int? Limit { get; set; }

        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            var parameters = new LinkedList<KeyValuePair<string, string>>();

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

            if (!string.IsNullOrEmpty(Label))
            {
                parameters.AddLast(new KeyValuePair<string, string>("label", Label));
            }

            if (Ids != null)
            {
                var joinedIds = string.Join(",", Ids.Where(id => !string.IsNullOrWhiteSpace(id)));
                if (!string.IsNullOrEmpty(joinedIds))
                {
                    parameters.AddLast(new KeyValuePair<string, string>("ids", joinedIds));
                }
            }

            if (!string.IsNullOrEmpty(Cursor))
            {
                parameters.AddLast(new KeyValuePair<string, string>("cursor", Cursor));
            }

            if (Limit.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("limit", Limit.Value.ToString()));
            }

            return parameters;
        }
    }
}
