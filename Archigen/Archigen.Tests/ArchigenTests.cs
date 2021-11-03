using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Archigen.Tests
{
    public class NameGenerator : IGenerator<string>
    {
        private Random Random = new Random();

        public string Next()
        {
            return Path.GetRandomFileName();
        }
    }

    public class NumberGenerator : IGenerator<int>
    {
        private Random Random = new Random();

        public int Next()
        {
            return this.Random.Next(10) + 1;
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Age { get; set; }
        public List<Ability> Abilities{ get; set; }

        public Character()
        {
            this.Abilities = new List<Ability>();
        }

    }

    public class Ability
    {
        public string Name { get; set; }
    }

    public struct TestStruct
    {
        public int NumericValue { get; set; }
        public object ReferenceValue { get; set; }
    }

    [TestClass]
    public class ArchigenTests
    {
        [TestMethod]
        public void Generator_CanInstantiatePrimitivesAndStructsAndObjects()
        {
            // A generator must be able to generate primitives
            var intGenerator = new Generator<int>();
            Assert.IsTrue(intGenerator.Next() == 0);

            // A generator must be able to generate structs
            var structGenerator = new Generator<TestStruct>();
            var s = structGenerator.Next();
            Assert.IsTrue(s.NumericValue == 0);
            Assert.IsNull(s.ReferenceValue);

            // A generator must be able to generate instances of a class
            var abilityGenerator = new Generator<Ability>();
            Ability ability = abilityGenerator.Next();
            Assert.IsNotNull(ability);
            Assert.IsNull(ability.Name);

            var characterGenerator = new Generator<Character>();
            var c = characterGenerator.Next();

            Assert.IsNotNull(c.Abilities);
        }


        [TestMethod]
        public void Generator_CanAssignProperties()
        {
            // A blank generator should not populate any properties of the class
            // or struct it is generating
            var abilityGenerator = new Generator<Ability>();
            Assert.IsTrue(string.IsNullOrEmpty(abilityGenerator.Next().Name));

            // The name property of Ability will be populated once we declare
            // an IGenerator<string> as the generating source. Syllabore's NameGenerator()
            // class implements that interface.
            abilityGenerator.ForProperty<string>(x => x.Name, new NameGenerator());
            Assert.IsFalse(string.IsNullOrEmpty(abilityGenerator.Next().Name));

            // Generator uses Dictionaries internally and should not throw an exceptions
            // if we redefine a property generator with a new generator
            abilityGenerator.ForProperty<string>(x => x.Name, new NameGenerator());
            Assert.IsFalse(string.IsNullOrEmpty(abilityGenerator.Next().Name));
        }

        [TestMethod]
        public void Generator_CanPopulateListProperties()
        {
            var names = new NameGenerator();
            var numbers = new NumberGenerator();

            // When using ForListProperty() we expect both the list itself
            // and the elements within the list to be instantiated.
            var g = new Generator<Character>()
                    .ForProperty<string>(x => x.Name, names)
                    .ForProperty<int>(x => x.Level, numbers)
                    .ForProperty<int>(x => x.Age, 30)
                    .ForListProperty<Ability>(x => x.Abilities, new Generator<Ability>()
                        .ForProperty<string>(x => x.Name, names))
                        .UsingSize(10)
                    .ForEach(x => x.Age++)
                    .ForEach(x => x.Age++);

            for (int i = 0; i < 1000; i++)
            {
                var character = g.Next();
                Assert.IsFalse(string.IsNullOrEmpty(character.Name));
                Assert.IsTrue(character.Level > 0 && character.Level <= 10);
                Assert.IsTrue(character.Abilities.Count == 10);
                Assert.IsTrue(character.Age == 32);

                foreach (var ability in character.Abilities)
                {
                    Assert.IsNotNull(ability.Name);
                }

            }

            

        }

    }
}
