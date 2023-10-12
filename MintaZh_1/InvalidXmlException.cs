using System;
using System.Runtime.Serialization;

namespace MintaZh_1
{
    public class InvalidXmlException : Exception
    {
        private const string DefaultMessage = "The parameters in the xml file were not given correctly: ";

        public InvalidXmlException(string message, Exception innerException) : base(DefaultMessage + message, innerException)
        {
        }

    }
}