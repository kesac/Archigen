using System;

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

    public class UserGenerator : IGenerator<User>
    {
        public User Next()
        {
            return new User() { FirstName = "John", LastName = "Smith", Age = 9001 };
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public User Partner { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Age + " " + Partner?.ToString();
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var g = new Generator<User>()
                    .ForProperty<string>(x => x.FirstName, new StringGenerator())
                    .ForProperty<string>(x => x.LastName, new StringGenerator())
                    .ForProperty<int>(x => x.Age, new NumberGenerator())
                    .ForProperty<User>(x => x.Partner, new UserGenerator());

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(g.Next());
            }

        }
    }
}
