using System;
using System.Runtime.Serialization;

namespace Casino
{
    public class FraudException : Exception
    {
        public FraudException()
            : base() { }
        public FraudException(string message)
            : base(message) { }
       
    }
}