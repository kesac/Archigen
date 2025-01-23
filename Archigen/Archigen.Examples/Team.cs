using System.Collections.Generic;

namespace Archigen.Examples
{
    /// <summary>
    /// Example team that contains <see cref="Player">Players</see>
    /// and is based out of a <see cref="Archigen.Examples.City"/>.
    /// </summary>
    public class Team
    {
        public string Name { get; set; }
        public City City { get; set; }
        public List<Player> Players { get; set; }
        public override string ToString() => this.Name;

    }
}
