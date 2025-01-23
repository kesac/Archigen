## Archigen
[![Nuget](https://img.shields.io/nuget/v/Archigen)](https://www.nuget.org/packages/Archigen/)

Archigen is a tiny class library for integrating other procedural generation libraries and programs together. It provides two basic things:
* An `IGenerator<T>` interface for content-generating classes to implement
* A `Generator<T>` class for chaining instances of `IGenerator<T>` together

The library also provides a small set of convenience classes that are useful when nesting generators:
 * `ConditionalGenerator<T>` for filtering the generated output space
 * `IWeighted<T>` and `WeightedSelector<T>` for weighted-random selection from lists 
 * `RandomSelector<T>` for basic random selection

## Example: Nesting Generators
Let's say you want to generate random teams of players as described by these classes:

```C#
public class Team
{
    public string Name { get; set; }
    public List<Player> Players { get; set; }
}

public class Player
{
    public string Name { get; set; }
}
```

Archigen will let you construct a generator for `Team` and nested `Player` objects like so:

```C#
var g = new Generator<Team>()
        .ForProperty<string>(x => x.TeamName, new NameGenerator())
        .ForListProperty<Player>(x => x.Players, new Generator<Player>()
            .ForProperty<string>(x => x.PlayerName, new NameGenerator()))
        .UsingSize(5);
```

This generates a new team and the players of the team every time `Next()` is called:

```C#
var team = g.Next(); 
```

The `NameGenerator` class used to populate `TeamName` and `PlayerName` can be anything that implements the `IGenerator<string>` interface and has a `Next()` method that returns a string. If you're looking for an actual name generator to use, see [Syllabore](https://github.com/kesac/Syllabore) which uses Archigen.

## Example: Simple Selectors

When generating a value for a nested property, you may want to simply select from a list rather than procedurally generate it. 
Archigen provides two selectors: `WeightedSelector<T>` and `RandomSelector<T>`. Both can be nested and chained in place of other generators.

### Weighted Selection
Let's say you have a City class that implements `IWeighted`:
```C#
public class City : IWeighted
{
    public string Name { get; set; }
    public int Population { get; set; }
    public int Weight { get => Population; set => Population = value; }
    
    public City(string name, int population)
    {
        Name = name;
        Population = population;
    }
}
```

You can create a weighted selector for cities like so:
```C#
var cities = new City[] { 
    new City("Astaria", 840000), 
    new City("Belarak", 420000), 
    new City("Crosgar", 210000) 
};

var citySelector = new WeightedSelector<City>(cities);
```
The `WeightedSelector<T>` can be nested and chained in place of procedural generators and return values with calls to `Next()`.

In this example, `Astaria` is twice more likely to be selected than `Belarak` and four times more likely than `Crosgar`.

### Random Selection

If you just want to randomly select from a list of options, you can use `RandomSelector<T>`:

```C#
var options = new string[] { "Mages", "Knights", "Dragons" };
var teamNames = new RandomSelector<string>(options); 
```

A `RandomSelector<T>` can be nested and chain in the place of procedural generators and return values with calls to `Next()`.




## Installation
Archigen is available as a NuGet package. You can install it from your [NuGet package manager in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio) or by running the following command in your NuGet package manager console:
```
Install-Package Archigen
```

## License
```
MIT License

Copyright (c) 2021-2025 Kevin Sacro

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
