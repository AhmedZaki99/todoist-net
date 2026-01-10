using System;
#if NETFRAMEWORK
using System.Runtime.Serialization;
#endif

using Todoist.Net.Models;

namespace Todoist.Net.Exceptions
{
    /// <summary>
    ///     Represents an errors that occur during requests to Todoist API.
    /// </summary>
    /// <seealso cref="System.Exception" />
#if NETFRAMEWORK
    [Serializable]
#endif
    public sealed class TodoistException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        public TodoistException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TodoistException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public TodoistException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="rawError">The raw error.</param>
        public TodoistException(int code, string message, CommandError rawError)
            : base(message)
        {
            Code = code;
            RawError = rawError;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public TodoistException(int code, string message, Exception inner)
            : base(message, inner)
        {
            Code = code;
        }

#if NETFRAMEWORK
        private TodoistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetInt32(nameof(Code));
            RawError = (CommandError)info.GetValue(nameof(RawError), typeof(CommandError));
        }
#endif

        /// <summary>
        ///     Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; }

        /// <summary>
        ///     Gets the raw error.
        /// </summary>
        /// <value>The raw error.</value>
        public CommandError RawError { get; }

#if NETFRAMEWORK
        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(Code), Code);
            info.AddValue(nameof(RawError), RawError);

            base.GetObjectData(info, context);
        }
#endif
    }
}
