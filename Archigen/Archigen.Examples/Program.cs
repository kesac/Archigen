using System;
using System.Linq;
using Archigen;

namespace Archigen.Examples
{

    /// <summary>
    /// Generates random teams with full
    /// ten named players each.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var g = new Generator<Team>()
                    .ForProperty<string>(x => x.TeamName, "Otherworldly Constants")
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
