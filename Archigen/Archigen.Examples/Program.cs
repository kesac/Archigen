using System;
using System.Linq;
using Archigen;

namespace Archigen.Examples
{
    /// <summary>
    /// An example program that shows how to use the Archigen library.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example 1:
            // Generate a team with 5 players and always name the team "Otherworldly Constants".
            // Generate random strings to serve as the name of each player.

            var playerGenerator = new Generator<Player>().ForProperty<string>(x => x.Name, new StringGenerator());
            var teamGenerator = new Generator<Team>()
                .ForProperty<string>(x => x.Name, "Otherworldly Constants")
                .ForListProperty<Player>(x => x.Players, playerGenerator)
                .UsingSize(5);

            DisplayToConsole(teamGenerator.Next()); // Creates team with 5 nested players

            // Example 2:
            // Change the output space so names of generated players always the letter 'a'.
            
            teamGenerator.ForListProperty<Player>(x => x.Players, new ConditionalGenerator<Player>(playerGenerator)
                .WithCondition(player => player.Name.Contains("a")))
                .UsingSize(5);

            DisplayToConsole(teamGenerator.Next()); // All players have names containing 'a'

            // Example 3:
            // Change the output space again, randomizing the team name from a set list
            var teamNames = new string[] { "Mages", "Knights", "Dragons" };
            var randomTeamNameGenerator = new RandomSelector<string>(teamNames);
            teamGenerator.ForProperty<string>(x => x.Name, randomTeamNameGenerator);

            DisplayToConsole(teamGenerator.Next()); // Still creates 5 nested players, but with a random team name


            // Example 4:
            // Adjust the output space and specify a city for the team.
            // Make larger cities more likely to be selected.
            var cities = new City[] { 
                new City("Astaria", 840000), 
                new City("Belarak", 420000), 
                new City("Crosgar", 210000) 
            };

            // Astaria is 2x more likely to be selected than Belarak and 4x more likely than Crosgar
            teamGenerator.ForProperty<City>(x => x.City, new WeightedSelector<City>(cities));

            DisplayToConsole(teamGenerator.Next()); // Still creates a team with players, but city is now provided

        }

        private static void DisplayToConsole(Team team)
        {
            Console.WriteLine();
            Console.WriteLine("Team:\t{0}", team);

            if(team.City != null)
            {
                Console.WriteLine("City:\t{0}", team.City);
            }

            foreach (var player in team.Players)
            {
                Console.WriteLine("Player:\t{0}", player);
            }
        }
    }
}
