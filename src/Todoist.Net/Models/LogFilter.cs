using System.Collections.Generic;
using System.Linq;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class LogFilter.
    /// </summary>
    public class LogFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogFilter"/> class.
        /// </summary>
        public LogFilter()
        {
            ObjectEventTypes = new LinkedList<ObjectEventTypes>();
        }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the initiator identifier.
        /// </summary>
        /// <value>The initiator identifier.</value>
        public string InitiatorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to filter activities with no initiator.
        /// </summary>
        public bool? InitiatorIdNull { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        /// <remarks>Default is 30, and the maximum is 100.</remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets the object event types.
        /// </summary>
        /// <value>The object event types.</value>
        /// <remarks>An alternative way to filter by multiple object and event types.
        /// When this parameter is specified the <see cref="ObjectId"/>, <see cref="EventType"/> and <see cref="ObjectId"/> parameters are ignored.</remarks>
        public ICollection<ObjectEventTypes> ObjectEventTypes { get; }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>The object identifier.</value>
        public string ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the parent item identifier.
        /// </summary>
        /// <value>The parent item identifier.</value>
        public string ParentItemId { get; set; }

        /// <summary>
        /// Gets or sets the parent project identifier.
        /// </summary>
        /// <value>The parent project identifier.</value>
        public string ParentProjectId { get; set; }

        /// <summary>
        /// <summary>
        /// Gets or sets a value indicating whether to include parent object activities.
        /// </summary>
        public bool? IncludeParentObject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include child object activities.
        /// </summary>
        public bool? IncludeChildObjects { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to annotate notes.
        /// </summary>
        public bool? AnnotateNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to annotate parents.
        /// </summary>
        public bool? AnnotateParents { get; set; }

        /// <summary>
        /// Gets or sets the cursor.
        /// </summary>
        public string Cursor { get; set; }

        // ReSharper disable once FunctionComplexityOverflow
        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            LinkedList<KeyValuePair<string, string>> parameters = new LinkedList<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(ObjectType))
            {
                parameters.AddLast(new KeyValuePair<string, string>("object_type", ObjectType));
            }

            if (!string.IsNullOrEmpty(ObjectId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("object_id", ObjectId));
            }

            if (!string.IsNullOrEmpty(EventType))
            {
                parameters.AddLast(new KeyValuePair<string, string>("event_type", EventType));
            }

            if (ObjectEventTypes.Any())
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("object_event_types", $"[{string.Join(",", ObjectEventTypes)}]"));
            }

            if (!string.IsNullOrEmpty(ParentProjectId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("parent_project_id", ParentProjectId));
            }

            if (!string.IsNullOrEmpty(ParentItemId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("parent_item_id", ParentItemId));
            }

            if (!string.IsNullOrEmpty(InitiatorId))
            {
                parameters.AddLast(new KeyValuePair<string, string>("initiator_id", InitiatorId));
            }

            if (InitiatorIdNull.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("initiator_id_null", InitiatorIdNull == true ? "true" : "false"));
            }

            if (IncludeParentObject.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("include_parent_object", IncludeParentObject == true ? "true" : "false"));
            }

            if (IncludeChildObjects.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("include_child_objects", IncludeChildObjects == true ? "true" : "false"));
            }

            if (AnnotateNotes.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("annotate_notes", AnnotateNotes == true ? "true" : "false"));
            }

            if (AnnotateParents.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("annotate_parents", AnnotateParents == true ? "true" : "false"));
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
