using System.Collections.Generic;

namespace Archigen.Examples
{
    /// <summary>
    /// Really basic POCO
    /// </summary>
    public class Team
    {
        public string TeamName { get; set; }
        public string Description { get; set; }
        public List<Player> Players { get; set; }
        public override string ToString() => this.TeamName;

    }
}
