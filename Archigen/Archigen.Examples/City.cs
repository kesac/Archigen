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
    public class City : IWeighted
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public int Weight { get => Population; set => Population = value; }
        public override string ToString() => this.Name;
    
        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }

    }
}
