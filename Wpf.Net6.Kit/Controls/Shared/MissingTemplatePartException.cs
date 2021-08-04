using System;
using System.Runtime.Serialization;

namespace Wpf.Net6.Kit.Controls.Shared
{
    public class MissingTemplatePartException : Exception
    {
        public string PartName { get; private set; } = string.Empty;
        public Type? PartType { get; private set; }

        public MissingTemplatePartException() { }

        public MissingTemplatePartException(string? message) : base(message) { }

        public MissingTemplatePartException(string partName, Type partType)
        {
            PartName = partName;
            PartType = partType;
        }

        public MissingTemplatePartException(string? message, Exception? innerException) : base(message, innerException) { }

        protected MissingTemplatePartException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}