# Cadru [![Build status](https://ci.appveyor.com/api/projects/status/3t0p4d04fyqtiun5?svg=true&retina=true)](https://ci.appveyor.com/project/scottdorman/cadru) [![Join the chat at https://gitter.im/scottdorman/cadru](https://badges.gitter.im/scottdorman/cadru.svg)](https://gitter.im/scottdorman/cadru?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

| | |
|-------|------|
|Cadru.AspNetCore|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.aspnetcore.svg)](http://www.nuget.org/packages/cadru.aspnetcore) [![NuGet version](https://img.shields.io/nuget/v/cadru.aspnetcore.svg)](http://www.nuget.org/packages/cadru.aspnetcore)|
|Cadru.Collections|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.collections.svg)](http://www.nuget.org/packages/cadru.collections) [![NuGet version](https://img.shields.io/nuget/v/cadru.collections.svg)](http://www.nuget.org/packages/cadru.collections)|
|Cadru.Contracts|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.contracts.svg)](http://www.nuget.org/packages/cadru.contracts) [![NuGet version](https://img.shields.io/nuget/v/cadru.contracts.svg)](http://www.nuget.org/packages/cadru.contracts)|
|Cadru.Core|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.core.svg)](http://www.nuget.org/packages/cadru.core) [![NuGet version](https://img.shields.io/nuget/v/cadru.core.svg)](http://www.nuget.org/packages/cadru.core)|
|Cadru.Core.Windows|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.core.windows.svg)](http://www.nuget.org/packages/cadru.core.windows) [![NuGet version](https://img.shields.io/nuget/v/cadru.core.windows.svg)](http://www.nuget.org/packages/cadru.core.windows)|
|Cadru.Data|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.data.svg)](http://www.nuget.org/packages/cadru.data) [![NuGet version](https://img.shields.io/nuget/v/cadru.data.svg)](http://www.nuget.org/packages/cadru.data)|
|Cadru.Data.Dapper|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.data.dapper.svg)](http://www.nuget.org/packages/cadru.data.dapper) [![NuGet version](https://img.shields.io/nuget/v/cadru.data.dapper.svg)](http://www.nuget.org/packages/cadru.data.dapper)|
|Cadru.IO|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.io.svg)](http://www.nuget.org/packages/cadru.io) [![NuGet version](https://img.shields.io/nuget/v/cadru.io.svg)](http://www.nuget.org/packages/cadru.io)|
|Cadru.Net|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.net.svg)](http://www.nuget.org/packages/cadru.net) [![NuGet version](https://img.shields.io/nuget/v/cadru.net.svg)](http://www.nuget.org/packages/cadru.net)|
|Cadru.Net.NetworkInformation|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.net.networkinformation.svg)](http://www.nuget.org/packages/cadru.net.networkinformation) [![NuGet version](https://img.shields.io/nuget/v/cadru.net.networkinformation.svg)](http://www.nuget.org/packages/cadru.net.networkinformation)|
|Cadru.Postal|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.postal.svg)](http://www.nuget.org/packages/cadru.postal) [![NuGet version](https://img.shields.io/nuget/v/cadru.postal.svg)](http://www.nuget.org/packages/cadru.postal)|
|Cadru.TransientFaultHandling|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.transientfaulthandling.svg)](http://www.nuget.org/packages/cadru.transientfaulthandling) [![NuGet version](https://img.shields.io/nuget/v/cadru.transientfaulthandling.svg)](http://www.nuget.org/packages/cadru.transientfaulthandling)|
|Cadru.UnitTest.Framework|[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.unittest.framework.svg)](http://www.nuget.org/packages/cadru.unittest.framework) [![NuGet version](https://img.shields.io/nuget/v/cadru.unittest.framework.svg)](http://www.nuget.org/packages/cadru.unittest.framework)|

## What is Cadru?
Cadru is a [collection of NuGet packages](https://www.nuget.org/packages?q=Tags%3A%22cadru%22) containing new APIs and extensions to the core .NET Framework to help complete your developer toolbox. It is designed to be cross-platform and the majority of the packages target [.NET Standard 1.0](https://docs.microsoft.com/en-us/dotnet/standard/library).

## What's in it?
Cadru is made up of the following assemblies:

* **Cadru.AspNetCore** - A utility framework containing new APIs and extensions to the .NET Framework.
* **Cadru.Collections** - Provides additional collection classes and extensions.
* **Cadru.Contracts** - Provides static classes for representing program contracts as preconditions in a way that's compatible with System.Diagnostics.Contracts.
* **Cadru.Core** - Provides common extensions and new APIs for the .NET Framework.
* **Cadru.Core.Windows** - A non-portable class library (targeting .NET Framework 4) meant for Windows desktop applications.
* **Cadru.Data** - Provides a standard way to read Excel data.
* **Cadru.Data.Dapper** - Provides a common database context and predicates for use with Dapper.
* **Cadru.IO** - Provides additional input and output (I/O) types.
* **Cadru.Net** - Provides transient error detection strategies for adding retry logic into your HttpClient calls and a UrlBuilder to help simplify building complex URLs.
* **Cadru.Net.NetworkInformation** - Provides access to network information and notification of network status changes.
* **Cadru.Postal** - Generate emails using ASP.NET MVC views.
* **Cadru.TransientFaultHandling** - Provides transient error detection strategies for adding retry logic into your application.
* **Cadru.UnitTest.Framework** - A non-portable class library (targeting .NET Framework 4) which adds additional assert capabilities for MSTest.

## Documentation
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
  * [Cadru.AspNetCore](http://www.nuget.org/packages/cadru.aspnetcore)
  * [Cadru.Collections](http://www.nuget.org/packages/cadru.collections)
  * [Cadru.Contracts](http://www.nuget.org/packages/cadru.contracts)
  * [Cadru.Core](http://www.nuget.org/packages/cadru.core)
  * [Cadru.Core.Windows](http://www.nuget.org/packages/cadru.core.windows)
  * [Cadru.Data](http://www.nuget.org/packages/cadru.data)
  * [Cadru.Data.Dapper](http://www.nuget.org/packages/cadru.data.dapper)
  * [Cadru.IO](http://www.nuget.org/packages/cadru.io)
  * [Cadru.Net](http://www.nuget.org/packages/cadru.net)
  * [Cadru.Net.NetworkInformation](http://www.nuget.org/packages/cadru.net.networkinformation)
  * [Cadru.Postal](http://www.nuget.org/packages/cadru.postal)
  * [Cadru.TransientFaultHandling](http://www.nuget.org/packages/cadru.transientfaulthandling)
  * [Cadru.UnitTest.Framework](http://www.nuget.org/packages/cadru.unittest.framework)


## Why another library?
Although Cadru was released as an open source framework in July 2013, some of the APIs it contains were created as early as 2003 and have been used in a variety of real world solutions. These are things that I kept having to rewrite in the apps I was building. Rather than continuing to rewrite them, I decided to encapsulate them in a library and make it broadly available. Although Cadru grew from app development, I have rewritten everything with a focus on being an API rather than a jumbled collection of utilities. That means everything is (hopefully) well documented, cleanly written, and easy to use.

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
