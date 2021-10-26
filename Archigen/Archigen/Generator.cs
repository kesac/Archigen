using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Archigen
{
    public class Generator<T> : IGenerator<T> where T : new()
    {
        public Dictionary<string, GeneratorInfo> Generators { get; set; }

        public Generator()
        {
            this.Generators = new Dictionary<string, GeneratorInfo>();
        }

        public Generator<T> ForProperty<U>(Expression<Func<T, U>> expression, IGenerator<U> generator)
        {
            var type = typeof(IGenerator<U>);
            var next = type.GetMethod("Next");

            var memberExpression = (MemberExpression) expression.Body;
            var property = (PropertyInfo) memberExpression.Member;
            
            this.Generators.Add(property.Name, new GeneratorInfo(generator, next));
            return this;
        }


        public T Next()
        {
            var result = new T();

            foreach(var targetProperty in this.Generators.Keys)
            {
                var property = result.GetType().GetProperty(targetProperty);
                var gInfo = this.Generators[targetProperty];

                property.SetValue(result, gInfo.Next.Invoke(gInfo.Generator, null));   
            }

            return result;
        }
    }
}
