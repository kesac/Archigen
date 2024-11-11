using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// Capable of randomly or procedurally generating
    /// instances of a specific class or struct.
    /// </summary>
    public interface IGenerator<T>
    {
        /// <summary>
        /// Returns a newly generated value.
        /// </summary>
        T Next();
    }

}
