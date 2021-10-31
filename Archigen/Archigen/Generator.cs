using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// Responsible for randomly generating
    /// objects of type <c>T</c> and initializing zero or more
    /// properties of <c>T</c> using other generators.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Generator<T> : IGenerator<T> where T : new()
    {
        private string LastPropertyDefined { get; set; }
        public Dictionary<string, GeneratorInfo> PropertyGenerators { get; set; }

        public Generator()
        {
            this.PropertyGenerators = new Dictionary<string, GeneratorInfo>();
        }

        /// <summary>
        /// Assigns a generator to the specified property of <c>T</c>. When new instances of <c>T</c>
        /// are instantiated through calls to <c>Next()</c>, the specified property of new instances 
        /// will be populated using the supplied generator.
        /// </summary>
        public Generator<T> ForProperty<U>(Expression<Func<T, U>> expression, IGenerator<U> generator)
        {
            var type = typeof(IGenerator<U>);
            var memberExpression = expression.Body as MemberExpression;
            var property = memberExpression?.Member as PropertyInfo;

            this.PropertyGenerators[property.Name] = new GeneratorInfo(generator, type);
            this.LastPropertyDefined = property.Name;
            return this;
        }

        /// <summary>
        /// Assigns a constant value to the specified property of <c>T</c>. When new instances of <c>T</c>
        /// are generated, this specific property will always be the same value.
        /// </summary>
        public Generator<T> ForProperty<U>(Expression<Func<T, U>> expression, U constant)
        {
            return this.ForProperty<U>(expression, new ConstantGenerator<U>(constant));
        }

        /// <summary>
        /// Assigns a generator for a specified List property of <c>T</c>. When new instances of <c>T</c>
        /// are instantiated through calls to <c>Next()</c>, the List property is first instantiated as a 
        /// generic List of type <c>U</c>, then elements in the list are instantiated with the supplied
        /// generator.
        /// </summary>
        public Generator<T> ForListProperty<U>(Expression<Func<T, List<U>>> expression, IGenerator<U> generator)
        {
            var parentType = typeof(IGenerator<List<U>>);
            var childType = typeof(IGenerator<U>);

            var gi = new GeneratorInfo(new Generator<List<U>>(), parentType);
            gi.ChildItemGenerator = new GeneratorInfo(generator, childType);

            var memberExpresion = expression.Body as MemberExpression;
            var property = memberExpresion?.Member as PropertyInfo;

            this.PropertyGenerators[property.Name] = gi;
            this.LastPropertyDefined = property.Name;
            return this;
        }

        /// <summary>
        /// Sets the desired element size of the last added property. This is only
        /// useful if the property was a list.
        /// </summary>
        public Generator<T> UsingSize(int size)
        {
            if(this.LastPropertyDefined != null && this.PropertyGenerators[this.LastPropertyDefined].ChildItemGenerator != null)
            {
                this.PropertyGenerators[this.LastPropertyDefined].ChildItemGenerator.Size = size;
            }
            return this;
        }

        /// <summary>
        /// Returns a new random instance of <c>T</c>. If this generator
        /// also has generators defined for the properties of <c>T</c>, then
        /// those properties will have random values as provided by their associated
        /// generator.
        /// </summary>
        public virtual T Next()
        {
            var result = new T();
            var type = result.GetType();

            foreach(var targetProperty in this.PropertyGenerators.Keys)
            {
                var property = type.GetProperty(targetProperty);
                var generatorInfo = this.PropertyGenerators[targetProperty];
                var propertyValue = generatorInfo.Invoke();

                property.SetValue(result, propertyValue);

                if (generatorInfo.ChildItemGenerator != null)
                {
                    for(int i = 0; i < generatorInfo.ChildItemGenerator.Size; i++)
                    {
                        ((IList)propertyValue).Add(generatorInfo.ChildItemGenerator.Invoke());
                    }
                }

            }

            return result;
        }

    }
}
