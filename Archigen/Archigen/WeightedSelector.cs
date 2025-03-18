using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Archigen
{
    /// <summary>
    /// A simple generator that returns <see cref="IWeighted"/> elements
    /// from a specified list. While elements are chosen at random,
    /// those with higher weights are more likely to be selected.
    /// </summary>
    public class WeightedSelector<T> : IGenerator<T>
    {
        private Random _random;

        /// <summary>
        /// The list of <see cref="IWeighted"/> elements that the generator will
        /// use to generate output during calls to <see cref="Next()"/>.
        /// Values with higher weights are more likely to be selected.
        /// </summary>
        public List<IWeighted> Values { get; set; }

        /// <summary>
        /// Creates an empty <see cref="WeightedSelector{T}"/>.
        /// </summary>
        [JsonConstructor]
        public WeightedSelector()
        {
            _random = new Random();
            Values = new List<IWeighted>();
        }

        /// <summary>
        /// Creates a new <see cref="WeightedSelector{T}"/>
        /// that will return random <see cref="IWeighted"/> elements
        /// from the specified values. Values with higher weights are more
        /// likely to be selected.
        /// </summary>
        public WeightedSelector(params T[] values) : this()
        {
            foreach (var value in values)
            {
                this.Add(value, 1);
            }
        }

        /// <summary>
        /// Creates a new <see cref="WeightedSelector{T}"/>
        /// that will return random <see cref="IWeighted"/> elements
        /// from the specified list. Values with higher weights are more
        /// likely to be selected.
        /// </summary>
        public WeightedSelector(IEnumerable<T> values) : this()
        {
            foreach(var value in values)
            {
                this.Add(value);
            }
        }


        /// <summary>
        /// Adds values that can be selected.
        /// </summary>
        public WeightedSelector<T> Add(T value)
        {
            if (value is IWeighted weighted)
            {
                this.Add(value, weighted.Weight);
            }
            else
            {
                this.Add(value, 1);
            }
            
            return this;
        }

        /// <summary>
        /// Adds values that can be selected. If the value implements the 
        /// IWeighted interface, the specified weight will be ignored in favor
        /// of the value's weight.
        /// </summary>
        public WeightedSelector<T> Add(T value, int weight)
        {
            if (value is IWeighted weighted)
            {
                Values.Add((IWeighted)value);
            }
            else
            {
                Values.Add(new Weighted<T>(value, weight));
            }

            return this;
        }

        /// <summary>
        /// Returns a random element from a list of 
        /// <see cref="IWeighted"/> values.
        /// </summary>
        public virtual T Next()
        {
            if (Values.Count == 0)
            {
                throw new InvalidOperationException("No items available to select.");
            }

            int totalWeight = Values.Sum(x => x.Weight);
            int randomSelection = _random.Next(totalWeight);

            int runningTotal = 0;

            for (int i = 0; i < Values.Count; i++)
            {
                runningTotal += Values[i].Weight;

                if (randomSelection < runningTotal)
                {
                    if(Values[i] is Weighted<T> weighted)
                    {
                        return weighted.Value;
                    }
                    else
                    {
                        return (T)Values[i];
                    }
                }
            }

            throw new InvalidOperationException("A random choice could not be made. The list may have items with non-positive weights.");

        }
    }
}
