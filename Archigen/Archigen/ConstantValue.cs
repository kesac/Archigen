using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// This is a convenience class used by <see cref="Generator{T}"/>
    /// when generating a complex object that requires a specific property
    /// to be a constant value across the entire output space.
    /// </summary>
    public class ConstantValue<T> : IGenerator<T>
    {
        private T Value;

        /// <summary>
        /// Creates a new generator that always returns the same value.
        /// </summary>
        public ConstantValue(T value) => this.Value = value;

        /// <summary>
        /// Gets the constant value of this generator.
        /// </summary>
        public T Next() => this.Value;
    }
}
