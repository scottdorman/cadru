# Cadru [![Build status](https://ci.appveyor.com/api/projects/status/3t0p4d04fyqtiun5?svg=true&retina=true)](https://ci.appveyor.com/project/scottdorman/cadru) 

[![Join the chat at https://gitter.im/scottdorman/cadru](https://badges.gitter.im/scottdorman/cadru.svg)](https://gitter.im/scottdorman/cadru?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

| | |
|-------|------|
|Cadru.Core|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.core.svg)](http://www.nuget.org/packages/cadru.core) [![NuGet version](https://img.shields.io/nuget/v/cadru.core.svg)](http://www.nuget.org/packages/cadru.core)|
|Cadru.Core.Windows|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.core.windows.svg)](http://www.nuget.org/packages/cadru.core.windows) [![NuGet version](https://img.shields.io/nuget/v/cadru.core.windows.svg)](http://www.nuget.org/packages/cadru.core.windows)|
|Cadru.Data.Dapper|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.data.dapper.svg)](http://www.nuget.org/packages/cadru.data.dapper) [![NuGet version](https://img.shields.io/nuget/v/cadru.data.dapper.svg)](http://www.nuget.org/packages/cadru.data.dapper)|
|Cadru.UnitTest.Framework|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.unittest.framework.svg)](http://www.nuget.org/packages/cadru.unittest.framework) [![NuGet version](https://img.shields.io/nuget/v/cadru.unittest.framework.svg)](http://www.nuget.org/packages/cadru.unittest.framework)|

## What is Cadru?
Cadru is a utility framework containing new APIs and extensions to the core .NET Framework
to help complete your developer toolbox. It is designed to be portable first, which means that the majority
of the library is available as a Portable Class Library (PCL) targeting the following frameworks:

* .NET Framework 4
* Silverlight 5
* Windows 8
* Windows Phone 8.1
* Windows Phone Silverlight 8

Any platform specific functionality is exposed as more narrowly focused portable class libraries or as a non portable library if that’s the only option.

## What's in it?
Cadru is made up of the following assemblies:

* **Cadru.Core** - A portable class library which provides the majority of the framework.
* **Cadru.Core.Windows** - A non-portable class library (targeting .NET Framework 4) meant for Windows desktop applications.
* **Cadru.UnitTest.Framework** - A non-portable class library (targeting .NET Framework 4) which adds additional assert capabilities for MSTest.

##Documentation
Documentation is available as .chm files hosted in the GitHub repository.

* [Cadru.Core](https://github.com/scottdorman/cadru/blob/master/docs/Help/Cadru.Core.Documentation.chm?raw=true)
* [Cadru.Core.Windows](https://github.com/scottdorman/cadru/blob/master/docs/Help/Cadru.Core.Windows.Documentation.chm?raw=true)
* [Cadru.UnitTest.Framework](https://github.com/scottdorman/cadru/blob/master/docs/Help/Cadru.UnitTest.Framework.Documentation.chm?raw=true)

There are also a lot of unit tests that show how to use the APIs which can be a good starting place as well. (My goal is to as be as close to 100% code coverage as possible. Obviously, that will always be a work in progress.) 

The long-term goal is to put the documentation online somewhere (probably as wiki pages hosted in the repository), but I don't have a time frame for when that will be complete.

## How do I get Cadru?
I’ve tried to make it easy to get Cadru for your own use.

* If you want the source code, you can [fork the repository](https://github.com/scottdorman/cadru/fork) or just browse it on GitHub.
* You can install one of the NuGet packages:
   * [Cadru.Core](https://www.nuget.org/packages/Cadru.Core/)
   * [Cadru.Core.Windows](https://www.nuget.org/packages/Cadru.Core.Windows/)
   * [Cadru.UnitTest.Framework](https://www.nuget.org/packages/Cadru.UnitTest.Framework/)

## Why another library?
Although Cadru was released as an open source framework in July 2013, some of the APIs it contains have 10 years of real world use behind them. These are things that I kept having to rewrite in the apps I was building. Rather than continuing to rewrite them, I decided to encapsulate them in a library and make it broadly available. Although Cadru grew from app development, I have rewritten everything with a focus on being an API rather than a jumbled collection of utilities. That means everything is (hopefully) well documented, cleanly written, and easy to use.

## Bugs and feature requests
Do you have a bug or a feature request? Please use the [issue tracker](https://github.com/scottdorman/cadru/issues) and search for existing and closed issues. If your problem or request isn't addressed yet, go ahead and [open a new issue](https://github.com/scottdorman/cadru/issues/new). 

## Contributing
You can also get involved and [fork the repository](https://github.com/scottdorman/cadru/fork) to submit your own pull requests. (More detailed contributor guidelines will be available soon.)

## Versioning
For transparency and to maintain backward compatibility (as much as possible), Cadru uses the [Semantic Versioning guidelines](http://semver.org/).

## Creators
* [Scott Dorman](http://about.me/scottdorman) ([@sdorman](http://twitter.com/sdorman)) - for the initial conception of the toolkit and core contributor.

## Copyright and license
Code and documentation copyright 2001-2016 Scott Dorman. Code is licensed under the [Microsoft Public License](http://opensource.org/licenses/Ms-PL.html), use it as you wish (but please 
provide some credit somewhere in your app.) Documentation is released under [Creative Commons](https://github.com/scottdorman/cadru/blob/master/docs/LICENSE).
