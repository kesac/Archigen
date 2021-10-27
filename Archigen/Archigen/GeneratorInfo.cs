using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// Utility class used by <see cref="Generator"/> to capture
    /// information required to call the <c>Next()</c> method of
    /// a specific instance of IGenerator.
    /// </summary>
    public class GeneratorInfo
    {
        public IGenerator Generator { get; set; }
        public MethodInfo Next { get; set; }
        public GeneratorInfo ChildItemGenerator { get; set; }
        public int? Size { get; set; }

        public GeneratorInfo(IGenerator g, Type type)
        {
            this.Generator = g;
            this.Next = type.GetMethod("Next");
        }

        public object Invoke()
        {
            return this.Next.Invoke(this.Generator, null);
        }

    }
}
