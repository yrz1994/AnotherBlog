using System;

namespace AnotherBlog.Domain.Core.Bus.Messages
{
    public abstract class Message
    {
        public Guid AggregateId { get; protected set; }

        public string MessageType { get; protected set; }

        public DateTime Timestamp { get; private set; }

        protected Message()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }
    }
}
