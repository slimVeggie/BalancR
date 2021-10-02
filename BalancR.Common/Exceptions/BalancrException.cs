using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BalancR.Common.Exceptions
{
    [Serializable]
    public class BalancrException : Exception
    {
        public BalancrException()
        {
        }

        public BalancrException(string message) : base(message)
        {
        }

        public BalancrException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BalancrException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
