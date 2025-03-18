using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Archigen
{
    /// <summary>
    /// A simple generator that returns elements
    /// from a specified list, chosen at random.
    /// </summary>
    public class RandomSelector<T> : IGenerator<T>
    {
        private Random _random;

        /// <summary>
        /// The list of values that the generator will
        /// use to generate output during calls to <see cref="Next()"/>.
        /// All values are equally likely to be selected.
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// Creates an empty <see cref="RandomSelector{T}"/>.
        /// </summary>
        [JsonConstructor]
        public RandomSelector()
        {
            _random = new Random();
            Values = new List<T>();
        }

        /// <summary>
        /// Creates a new <see cref="RandomSelector{T}"/>
        /// that will return a random item from the arguments.
        /// </summary>
        public RandomSelector(params T[] values) : this()
        {
            Values.AddRange(values);
        }

        /// <summary>
        /// Creates a new <see cref="RandomSelector{T}"/>
        /// that will return random values from the specified list.
        /// </summary>
        public RandomSelector(IEnumerable<T> values) : this()
        {
            Values.AddRange(values);
        }

        /// <summary>
        /// Adds values that can be selected.
        /// </summary>
        public RandomSelector<T> Add(params T[] values)
        {
            Values.AddRange(values);
            return this;
        }

        /// <summary>
        /// Returns a random value from a list of values.
        /// </summary>
        public virtual T Next()
        {
            if (Values.Count == 0)
            {
                throw new InvalidOperationException("No items available to select.");
            }

            return Values[_random.Next(Values.Count)];
        }
    }
}
