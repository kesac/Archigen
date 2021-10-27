using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    public interface IGenerator { }
 
    /// <summary>
    /// Capable of randomly or procedurally generating
    /// instances of a specific class or struct.
    /// </summary>
    public interface IGenerator<T> : IGenerator
    {
        T Next();
    }

}
