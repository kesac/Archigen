using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Archigen;

namespace Archigen.Examples
{
    /// <summary>
    /// A random string generator.
    /// </summary>
    public class StringGenerator : IGenerator<string>
    {
        private Random Random = new Random();

        public string Next()
        {
            var result = new StringBuilder();

            for(int i = 0; i < 8; i++)
            {
                result.Append((char)this.Random.Next('a', 'z'));
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// Really basic POCO
    /// </summary>
    public class Team
    {
        public string TeamName { get; set; }
        public List<Player> Players { get; set; }
        public override string ToString() => this.TeamName;

    }

    /// <summary>
    /// Nested POCO
    /// </summary>
    public class Player
    {
        public string PlayerName { get; set; }

        public override string ToString() => this.PlayerName;

    }

    /// <summary>
    /// Generates random teams with full
    /// ten named players each.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var g = new Generator<Team>()
                    .ForProperty<string>(x => x.TeamName, new StringGenerator())
                    .ForListProperty<Player>(x => x.Players, new Generator<Player>()
                        .ForProperty<string>(x => x.PlayerName, new StringGenerator()))
                    .UsingSize(10);

            var c = new ConditionalGenerator<Team>(g)
                                  .WithCondition(team => team.TeamName.Contains("a"));

            for(int i = 0; i < 3; i++)
            {
                var team = c.Next();
                Console.WriteLine("Team {0}", team);

                foreach(var player in team.Players)
                {
                    Console.WriteLine("Player {0}", player);
                }
            }
        }
    }
}
