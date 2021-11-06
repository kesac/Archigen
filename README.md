## Archigen
[![Nuget](https://img.shields.io/nuget/v/Archigen)](https://www.nuget.org/packages/Archigen/)

Archigen is a tiny class library for integrating other procedural generation libraries and programs together. It provides two things:
* An `IGenerator<T>` interface for content-generating classes to implement
* A `Generator<T>` class for chaining instances of `IGenerator<T>` together

Archigen is a dependency of [Syllabore](https://github.com/kesac/Syllabore) and [Loremaker](https://github.com/kesac/Loremaker).

## Example
Let's say you want to generate random teams of players as described by these two classes:

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

Archigen will you construct a generator for the `Team` class like so:

```C#
var g = new Generator<Team>()
        .ForProperty<string>(x => x.TeamName, new StringGenerator())
        .ForListProperty<Player>(x => x.Players, new Generator<Player>()
            .ForProperty<string>(x => x.PlayerName, new StringGenerator()))
        .UsingSize(10);
```

Then you'd randomly generate teams of players like so:

```C#
var team = g.Next(); 
```

The `StringGenerator` class used to populate `TeamName` and `PlayerName` can be anything as long as it implements the `IGenerator<string>` interface. For example, this would work:

```C#
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
```

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
