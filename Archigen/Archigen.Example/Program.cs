using Syllabore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archigen.Example
{

    public class StringGenerator : IGenerator<string>
    {
        public string Next()
        {
            return "This is a string";
        }
    }

    public class NumberGenerator : IGenerator<int>
    {
        public int Next()
        {
            return 42;
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<User> Friends { get; set; }

        public User() { this.Friends = new List<User>(); }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append(FirstName + " " + LastName + " , age: " + Age + ", total friends: " + Friends.Count);

            foreach(var friend in Friends)
            {
                result.AppendLine();
                result.Append(friend.ToString());
            }

            return result.ToString();
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {

            var names = new NameGenerator();

            var g = new Generator<User>()
                    .ForProperty<string>(x => x.FirstName, names)
                    .ForProperty<string>(x => x.FirstName, names)
                    .ForProperty<string>(x => x.LastName, names)
                    .ForProperty<int>(x => x.Age, new NumberGenerator())
                    .ForListProperty<User>(x => x.Friends, new Generator<User>()
                        .ForProperty<string>(x => x.FirstName, names)
                        .ForProperty<string>(x => x.LastName, names))
                        .UsingSize(2);

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(g.Next());
            }

        }
    }
}
