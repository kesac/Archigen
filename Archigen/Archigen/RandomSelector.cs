using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// A simple generator that returns elements
    /// from a specified list, chosen at random.
    /// </summary>
    public class RandomSelector<T> : IGenerator<T>
    {
        protected Random _random;

        /// <summary>
        /// The list of values that the generator will
        /// use to generate output during calls to <see cref="Next()"/>.
        /// All values are equally likely to be selected.
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// Creates a new <see cref="RandomSelector{T}"/>
        /// that will return random values from the specified list.
        /// </summary>
        public RandomSelector(IEnumerable<T> values)
        {
            _random = new Random();
            Values = new List<T>(values);
        }

        /// <summary>
        /// Returns a random value from a list of values.
        /// </summary>
        public virtual T Next()
        {
            return Values[_random.Next(Values.Count)];
        }
    }
}
