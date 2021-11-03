using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// This is a convience class used by <see cref="Generator{T}"/>
    /// when generating a complex object that requires a specific property
    /// to be a constant value across the entire output space.
    /// </summary>
    public class ConstantGenerator<T> : IGenerator<T>
    {
        private T Value;

        public ConstantGenerator(T value) => this.Value = value;

        public T Next() => this.Value;
    }
}
