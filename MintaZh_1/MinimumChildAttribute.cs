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

        /// <summary>
        /// Sets the minimum acceptable value for child property.
        /// </summary>
        /// <param name="value">The minimum value.</param>
        public MinimumChildAttribute(int value)
        {
            Value = value;
        }
    }
}
