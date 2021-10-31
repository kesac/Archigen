using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// This is a convience class to be used <see cref="Generator{T}"/>
    /// when you are generating a complex object and a specific property
    /// needs to be the same value across the entire output space.
    /// </summary>
    public class ConstantGenerator<T> : IGenerator<T>
    {
        private T Value;

        public ConstantGenerator(T value) => this.Value = value;

        public T Next() => this.Value;
    }
}
