using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// This is a special type of generator that monitors
    /// its output and checks whether it meets specified
    /// criteria. It is useful when wanting to avoid overly exotic
    /// output. Be careful making constraints:
    /// this generator will throw an InvalidOperationException if it cannot
    /// generate any output that meets all conditions.
    /// </summary>
    public class ConditionalGenerator<T> : IGenerator<T>
    {
        public IGenerator<T> Generator { get; set; }
        public List<Func<T, bool>> PostGenerationConditions { get; set; }
        public int RetryLimit { get; set; }

        /// <summary>
        /// Constructs a conditional generator with the specified <see cref="IGenerator{T}"/>
        /// as the source.
        /// </summary>
        public ConditionalGenerator(IGenerator<T> generator)
        {
            this.Generator = generator;
            this.PostGenerationConditions = new List<Func<T, bool>>();
            this.RetryLimit = 100;
        }

        /// <summary>
        /// Adds a condition for that generated output must
        /// always satisfy.
        /// </summary>
        public ConditionalGenerator<T> WithCondition(Func<T, bool> condition)
        {
            this.PostGenerationConditions.Add(condition);
            return this;
        }

        /// <summary>
        /// Limits the number of generation retries to the specified number.
        /// The default number of retries is 100. If a call to <see cref="Next()"/>
        /// cannot product valid output within this number of retries, it will
        /// throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        public ConditionalGenerator<T> LimitRetries(int retries)
        {
            this.RetryLimit = 100;
            return this;
        }

        /// <summary>
        /// Generates next output.
        /// Throws <see cref="InvalidOperationException"/> if it cannot
        /// produce any output that meets previously specified conditions.
        /// </summary>
        public T Next()
        {
            var result = this.Generator.Next();
            var retries = 0;

            while (!this.PostGenerationConditions.TrueForAll(condition => condition(result)))
            {
                result = this.Generator.Next();
                retries++;

                if(retries > this.RetryLimit)
                {
                    throw new InvalidOperationException("Could not generate a result that could pass all conditions.");
                }

            }

            return result;
        }
    }
}
