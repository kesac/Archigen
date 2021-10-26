using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Archigen
{
    public class GeneratorInfo
    {
        public IGenerator Generator { get; set; }
        public MethodInfo Next { get; set; }

        public GeneratorInfo(IGenerator g, MethodInfo next)
        {
            this.Generator = g;
            this.Next = next;
        }

    }
}
