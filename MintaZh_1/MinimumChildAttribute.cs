using System;

namespace MintaZh_1
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinimumChildAttribute : Attribute
    {
        /// <summary>
        /// The set minimum value.
        /// </summary>
        public readonly int Value;

        public MinimumChildAttribute(int value)
        {
            Value = value;
        }
    }
}
