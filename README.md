## Archigen
[![Nuget](https://img.shields.io/nuget/v/Archigen)](https://www.nuget.org/packages/Archigen/)

Archigen is a small class library for integrating other procedural generation libraries and programs together. It provides two basic things:
* A `Generator<T>` class for linking procedural generators together
* An `IGenerator<T>` interface for procedural generators to use, if they want to support chaining

## Linking Procedural Generators
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

The `NameGenerator` class used to populate `TeamName` and `PlayerName` can be anything that 
implements the `IGenerator<string>` interface and has a `Next()` method that returns a string. 
If you're looking for an existing name generator to use, 
see [Syllabore](https://github.com/kesac/Syllabore) which uses Archigen.

## Random Selectors

Not all nested properties need to be procedurally generated. 
Often, random selection is sufficient.
This library provides the following for handling random selection: 
 * `RandomSelector<T>` for basic random selection
 * `WeightedSelector<T>` for weighted random selection (which allows some elements are selected more often than others)
 * `IWeighted<T>` for weighted classes to use
 
Let's say you have a class that describes cities:
```C#
    public class City
    {
        public string Name { get; set; }
    
        public City(string name)
        {
            Name = name;
        }
    }
```

You can create a weighted selector for cities like so:
```C#
var citySelector = new WeightedSelector<City>();
citySelector.Add(new City("Astaria"), 4);
citySelector.Add(new City("Belarak"), 2);
citySelector.Add(new City("Crosgar"), 1);
```
In this example, `Astaria` is twice more likely to be selected than `Belarak` and four times more likely than `Crosgar`.

The `WeightedSelector<T>` can be nested and added in places where any `IGenerator<City>` is expected. 
It returns a `City` with calls to `Next()`.


### Random Selection

If you just want to randomly select from a list of options, you can use `RandomSelector<T>`:

```C#
var teamNames = new RandomSelector<string>("Mages", "Knights", "Dragons"); 
```

Like its weighted variant, a `RandomSelector<T>` can be nested and used in the place of other procedural generators and return values with calls to `Next()`.


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
