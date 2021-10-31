## Archigen
[![Nuget](https://img.shields.io/nuget/v/Archigen)](https://www.nuget.org/packages/Archigen/)

Archigen is a tiny class library for integrating other procedural generation libraries and programs together. It provides two things:
* An `IGenerator<T>` interface for content-generating classes to implement
* A `Generator<T>` class for chaining instances of `IGenerator<T>` together

Archigen is a dependency of [Syllabore](https://github.com/kesac/Syllabore) and [Loremaker](https://github.com/kesac/Loremaker).

## Example
Say you have a couple classes you want to randomly generate instances of together:

```C#
public class Team
{
    public string TeamName { get; set; }
    public List<Players> Players { get; set; }
}

public class Player
{
    public string PlayerName { get; set; }
}
```

Providing you already made a custom name generation class that implements `IGenerator<string>`, you can construct a chained generator for teams and players like so:

```C#
var g = new Generator<Team>()
        .ForProperty<string>(x => x.TeamName, new NameGenerator())
        .ForListProperty<Player>(x => x.Players, new Generator<Player>()
            .ForProperty<string>(x => x.PlayerName, new NameGenerator()))
        .UsingSize(10);
```

Then you'll be able to randomly generate teams of players like so:

```C#
var team = g.Next(); 
```
Each generated `Team` will have its `TeamName` property populated, the `Players` property instantiated to a List of type `Player`, and 10 randomly generated `Players` list added to that list. Additionally, each generated `Player` will also have their `PlayerName` property populated.


## Installation
Archigen is available as a NuGet package. You can install it from your [NuGet package manager in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio) or by running the following command in your NuGet package manager console:
```
Install-Package Archigen
```

## License
```
MIT License

Copyright (c) 2021 Kevin Sacro

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
