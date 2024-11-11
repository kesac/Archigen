using System;
using System.Text;

namespace Archigen.Examples
{
    /// <summary>
    /// A random string generator.
    /// </summary>
    public class StringGenerator : IGenerator<string>
    {
        private Random Random = new Random();

        public string Next()
        {
            var result = new StringBuilder();

            for(int i = 0; i < 8; i++)
            {
                result.Append((char)this.Random.Next('a', 'z'));
            }

            return result.ToString();
        }
    }
}
