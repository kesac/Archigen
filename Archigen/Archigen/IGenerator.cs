using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{

    public interface IGenerator { }

    // Represents anything that can procedurally
    // generate something
    public interface IGenerator<T> : IGenerator
    {
        T Next();
    }
}
