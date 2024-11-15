using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen
{
    /// <summary>
    /// <para>
    /// Represents an element that can
    /// be randomly selected from a list and has 
    /// a weight value that affects how frequently it
    /// should be selected compared to others.
    /// </para>
    /// </summary>
    public interface IWeighted
    {
        /// <summary>
        /// <para>
        /// A value indicating how more frequently this instance of 
        /// <see cref="IWeighted"/> should be selected over other instances.
        /// </para>
        /// <para>
        /// For example, if two <see cref="IWeighted">IWeighteds</see> 
        /// x and y have the weights 1 and 4 respectively then
        /// y should be selected four times as likely as x.
        /// </para>
        /// </summary>
        int Weight { get; set; }
    }
}
