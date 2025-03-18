using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Archigen.Examples
{
    /// <summary>
    /// Example city where the population is used as the selection weight.
    /// </summary>
    public class City
    {
        public string Name { get; set; }
    
        public City(string name)
        {
            Name = name;
        }
    }
}
