## Archigen
[![Nuget](https://img.shields.io/nuget/v/Archigen)](https://www.nuget.org/packages/Archigen/)

Archigen is a tiny class library for integrating other procedural generation libraries and programs together. It provides a few basic things:
* An `IGenerator<T>` interface for content-generating classes to implement
* A `Generator<T>` class for chaining instances of `IGenerator<T>` together

In addition, a `ConditionalGenerator<T>` class is also provided when constraining generated output is necessary.

## Example
Let's say you want to generate random teams of players as described by these two classes:

```C#
public class Team
{
    public string TeamName { get; set; }
    public List<Player> Players { get; set; }
}

public class Player
{
    public string PlayerName { get; set; }
}
```

Archigen will let you construct a generator for the `Team` class like so:

```C#
var g = new Generator<Team>()
        .ForProperty<string>(x => x.TeamName, new NameGenerator())
        .ForListProperty<Player>(x => x.Players, new Generator<Player>()
            .ForProperty<string>(x => x.PlayerName, new NameGenerator()))
        .UsingSize(10);
```

And generate a new team of players every time `Next()` is called:

```C#
var team = g.Next(); 
```

The `NameGenerator` class used to populate `TeamName` and `PlayerName` can be anything as long as it implements the `IGenerator<string>` interface and has a `Next()` method that returns a string. If you're looking for an actual name generator to use, see [Syllabore](https://github.com/kesac/Syllabore) which uses Archigen.

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
