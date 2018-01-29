<p align="center">
  <img src="HttpPatch.png" alt="HttpPatch" width="250"/>
</p>

[![AppVeyor](https://ci.appveyor.com/api/projects/status/8sq80lyqcatsnssy?svg=true)](https://ci.appveyor.com/project/Marusyk/simple-httppatch) [![Travis build status](https://img.shields.io/travis/Marusyk/Simple.HttpPatch.svg?label=travis-ci&branch=master&style=flat-square)](https://travis-ci.org/Marusyk/Simple.HttpPatch) [![GitHub (pre-)release](https://img.shields.io/github/release/Marusyk/Simple.HttpPatch/all.svg)](https://github.com/Marusyk/Simple.HttpPatch/releases/tag/v1.0.0-beta)  [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Simple.HttpPatch.svg)](https://www.nuget.org/packages/Simple.HttpPatch) 
[![NuGet](https://img.shields.io/nuget/dt/Simple.HttpPatch.svg)]()
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) ![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)

# Simple.HttpPatch

Simple.HttpPatch is implementation for .NET (Full framework and Core) to easily allow & apply partial RESTful service (through Web API) using [HTTP PATCH](https://tools.ietf.org/html/rfc5789) method.

## Installation

You can install the latest version via [NuGet](https://www.nuget.org/packages/Simple.HttpPatch/).

`PM> Install-Package Simple.HttpPatch`

## How to use

See [samples](https://github.com/Marusyk/Simple.HttpPatch/tree/master/samples/Simple.HttpPatch.Samples) folder to learn of to use this library with ASP.NET Core.

Patch a single entity

```C#
[HttpPatch]
public Person Patch([FromBody] Patch<Person> personPatch)
{
    var person = _repo.GetPersonById(1);
    personPatch.Apply(person);
    return person;
}
```

To exclude properties of an entity while applying the changes to the original entity use `PatchIgnoreAttribute`. 
When your property is a reference type (which allows null) but you don't want that null overwrites your previous stored data then use  `PatchIgnoreNullAttribute`

```C#
public class Person
{
    public int Id { get; set; }
    [PatchIgnore]
    public string Name { get; set; }
    public int? Age { get; set; }
    [PatchIgnoreNull]
    public DateTime BirthDate { get; set; }
}
```

*Note: The property with name `Id` is excluded by default*

 For firewalls that don't support `PATCH` see [this issue](https://github.com/Marusyk/Simple.HttpPatch/issues/5)
 
 ## Contributing

Please read [CONTRIBUTING.md](https://github.com/Marusyk/Simple.HttpPatch/blob/master/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## License

This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/Marusyk/Simple.HttpPatch/blob/master/LICENSE) file for details
