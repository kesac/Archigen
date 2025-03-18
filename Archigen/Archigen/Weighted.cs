using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// A wrapper for elements that require a weight value
    /// but do not have one by default.
    /// </summary>
    public class Weighted<T> : IWeighted
    {
        /// <summary>
        /// A value indicating how more frequently this instance 
        /// is to be chosen over other <see cref="IWeighted"/> instances.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// The value that this instance wraps.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="Weighted{T}"/> instance.
        /// </summary>
        public Weighted(T value, int weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}
