using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// A simple generator that returns <see cref="IWeighted"/> elements
    /// from a specified list. While elements are chosen at random,
    /// those with higher weights are more likely to be selected.
    /// </summary>
    public class WeightedSelector<T> : IGenerator<T> where T : IWeighted
    {
        protected Random _random;

        /// <summary>
        /// The list of <see cref="IWeighted"/> elements that the generator will
        /// use to generate output during calls to <see cref="Next()"/>.
        /// Values with higher weights are more likely to be selected.
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// Creates a new <see cref="WeightedSelector{T}"/>
        /// that will return random <see cref="IWeighted"/> elements
        /// from the specified list. Values with higher weights are more
        /// likely to be selected.
        /// </summary>
        public WeightedSelector(IEnumerable<T> values)
        {
            _random = new Random();
            Values = new List<T>(values);
        }

        /// <summary>
        /// Returns a random element from a list of 
        /// <see cref="IWeighted"/> values.
        /// </summary>
        public virtual T Next()
        {
            int totalWeight = Values.Sum(x => x.Weight);
            int randomSelection = _random.Next(totalWeight);

            int runningTotal = 0;

            for (int i = 0; i < Values.Count; i++)
            {
                runningTotal += Values[i].Weight;

                if (randomSelection < runningTotal)
                {
                    return Values[i];
                }
            }

            throw new InvalidOperationException("A random choice could not be made. The list may be empty or there are elements with non-positive weights.");

        }
    }
}
