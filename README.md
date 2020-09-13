# Cadru [![Build status](https://ci.appveyor.com/api/projects/status/3t0p4d04fyqtiun5?svg=true&retina=true)](https://ci.appveyor.com/project/scottdorman/cadru) [![Join the chat at https://gitter.im/scottdorman/cadru](https://badges.gitter.im/scottdorman/cadru.svg)](https://gitter.im/scottdorman/cadru?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## What is Cadru?
Cadru is a [collection of NuGet packages](https://www.nuget.org/packages?q=Tags:"cadru") containing new APIs and extensions to the core .NET Framework to help complete your developer toolbox. It is designed to be cross-platform and targets [.NET Standard 2.1](https://docs.microsoft.com/en-us/dotnet/standard/library).

## What's in it?
Cadru is made up of the following packages:

### Cadru.AspNetCore
Provides ASP.NET Core middleware and other extensions for request and/response logging.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.aspnetcore.svg)](http://www.nuget.org/packages/cadru.aspnetcore) [![NuGet version](https://img.shields.io/nuget/v/cadru.aspnetcore.svg)](http://www.nuget.org/packages/cadru.aspnetcore)

### Cadru.AspNetCore.Mvc
Provides additional support for working with ASP.NET Core, such as custom view location expanders, support for [IMetadataAware](https://docs.microsoft.com/en-us/dotnet/api/system.web.modelbinding.imetadataaware), and rendering enumerated types as SelectLists based on a [UiHint](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.uihintattribute) attribute, and extensions to make working with state management a bit simpler.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.aspnetcore.mvc.svg)](http://www.nuget.org/packages/cadru.aspnetcore.mvc) [![NuGet version](https://img.shields.io/nuget/v/cadru.aspnetcore.mvc.svg)](http://www.nuget.org/packages/cadru.aspnetcore.mvc)

### Cadru.AspNetCore.Mvc.TagHelpers
Provides additional ASP.NET Core TagHelpers.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.aspnetcore.mvc.taghelpers.svg)](http://www.nuget.org/packages/cadru.aspnetcore) [![NuGet version](https://img.shields.io/nuget/v/cadru.aspnetcore.mvc.taghelpers.svg)](http://www.nuget.org/packages/cadru.aspnetcore.mvc.taghelpers)

### Cadru.Build.Tasks
Provides additiona MSBuild tasks and is used to support [assembly-build-versioning](https://github.com/scottdorman/assembly-build-versioning).

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.build.tasks.svg)](http://www.nuget.org/packages/cadru.build.tasks) [![NuGet version](https://img.shields.io/nuget/v/cadru.build.tasks.svg)](http://www.nuget.org/packages/cadru.build.tasks)

### Cadru.Caching
Provides a standard implementation for creating and using cache keys.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.caching.svg)](http://www.nuget.org/packages/cadru.caching) [![NuGet version](https://img.shields.io/nuget/v/cadru.caching.svg)](http://www.nuget.org/packages/cadru.caching)

### Cadru.Collections
Provides additional collection classes and extensions.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.collections.svg)](http://www.nuget.org/packages/cadru.collections) [![NuGet version](https://img.shields.io/nuget/v/cadru.collections.svg)](http://www.nuget.org/packages/cadru.collections)

### Cadru.Contracts
Provides static classes for representing program contracts as preconditions in a way that's compatible with System.Diagnostics.Contracts.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.contracts.svg)](http://www.nuget.org/packages/cadru.contracts) [![NuGet version](https://img.shields.io/nuget/v/cadru.contracts.svg)](http://www.nuget.org/packages/cadru.contracts)

### Cadru.Core
Provides common extensions and new APIs for the .NET Framework.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.core.svg)](http://www.nuget.org/packages/cadru.core) [![NuGet version](https://img.shields.io/nuget/v/cadru.core.svg)](http://www.nuget.org/packages/cadru.core)

### Cadru.Data
Provides a standard way to read Excel data and fixed width files.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.data.svg)](http://www.nuget.org/packages/cadru.data) [![NuGet version](https://img.shields.io/nuget/v/cadru.data.svg)](http://www.nuget.org/packages/cadru.data)

### Cadru.Data.Annotations
Provides common data annotation attributes.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.data.annotations.svg)](http://www.nuget.org/packages/cadru.data.annotations) [![NuGet version](https://img.shields.io/nuget/v/cadru.data.annotations.svg)](http://www.nuget.org/packages/cadru.data.annotations)

### Cadru.Data.Dapper
Provides a common database context and predicates for use with Dapper.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.data.dapper.svg)](http://www.nuget.org/packages/cadru.data.dapper) [![NuGet version](https://img.shields.io/nuget/v/cadru.data.dapper.svg)](http://www.nuget.org/packages/cadru.data.dapper)

### Cadru.Environment
Provides support for determining framework versions, IIS version, and feature detection.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.environment.svg)](http://www.nuget.org/packages/cadru.environment) [![NuGet version](https://img.shields.io/nuget/v/cadru.environment.svg)](http://www.nuget.org/packages/cadru.environment)

### Cadru.Extensions.FileProviders
Additional file providers and support for working with physical files and directories.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.extensions.fileproviders.svg)](http://www.nuget.org/packages/cadru.extensions.fileproviders) [![NuGet version](https://img.shields.io/nuget/v/cadru.extensions.fileproviders.svg)](http://www.nuget.org/packages/cadru.extensions.fileproviders)

### Cadru.Net.Http
Provides transient error detection strategies for adding retry logic into your HttpClient calls and a UrlBuilder to help simplify building complex URLs.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.net.http.svg)](http://www.nuget.org/packages/cadru.net.http) [![NuGet version](https://img.shields.io/nuget/v/cadru.net.http.svg)](http://www.nuget.org/packages/cadru.net.http)

### Cadru.Net.NetworkInformation
Provides access to network information and notification of network status changes.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.net.networkinformation.svg)](http://www.nuget.org/packages/cadru.net.networkinformation) [![NuGet version](https://img.shields.io/nuget/v/cadru.net.networkinformation.svg)](http://www.nuget.org/packages/cadru.net.networkinformation)

### Cadru.Polly
Provides support for working with [Polly](https://github.com/App-vNext/Polly) policies, including a strategy for resilient database queries.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.polly.svg)](http://www.nuget.org/packages/cadru.polly) [![NuGet version](https://img.shields.io/nuget/v/cadru.polly.svg)](http://www.nuget.org/packages/cadru.polly)

### Cadru.Postal
Provides classes for generating email using ASP.NET MVC Razor views.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.postal.svg)](http://www.nuget.org/packages/cadru.postal) [![NuGet version](https://img.shields.io/nuget/v/cadru.postal.svg)](http://www.nuget.org/packages/cadru.postal)

### Cadru.Scim
Support for creating System for Cross-domain Identity Management (SCIM) filters.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.scim.svg)](http://www.nuget.org/packages/cadru.scim) [![NuGet version](https://img.shields.io/nuget/v/cadru.scim.svg)](http://www.nuget.org/packages/cadru.scim)

### Cadru.UnitTest.Framework
Provides additional assert capabilities for MS Test.

[![NuGet downloads](https://img.shields.io/nuget/dt/cadru.unittest.framework.svg)](http://www.nuget.org/packages/cadru.unittest.framework) [![NuGet version](https://img.shields.io/nuget/v/cadru.unittest.framework.svg)](http://www.nuget.org/packages/cadru.unittest.framework)

#### The following packages have been retired. They are still available on NuGet, but shouldn't be used.
* **Cadru.TransientFaultHandling** - This package has been retired and should be replaced with **Cadru.Polly*.
* **Cadru.Net** - This package is renamed to **Cadru.Net.Http*
* **Cadru.Core.Windows** - This package is retired, and features migrated to **Cadru.Environment**, **Cadru.IO**, and **Cadru.Net.NetworkInformation** as appropriate.
* **Cadru.IO** - This package will eventually be retired and replaced with **Cadru.Extensions.FileProviders**.

## What's next for Cadru?
The most significant update planned will be support for .NET 5 when it releases in November. Until then, there are a handful of known tasks still left. See the [Cadru 5.0](https://github.com/scottdorman/cadru/milestone/1) milestone for more details. There are only a few issues in there right now, but I will be adding more over time.

## Why another library?
Although Cadru was released as an open source framework in July 2013, some of the APIs it contains were created as early as 2003 and have been used in a variety of real world solutions. These are things that I kept having to rewrite in the apps I was building. Rather than continuing to rewrite them, I decided to encapsulate them in a library and make it broadly available. Although Cadru grew from app development, I have rewritten everything with a focus on being an API rather than a jumbled collection of utilities. That means everything is (hopefully) well documented, cleanly written, and easy to use.

## Documentation
There are also a lot of unit tests that show how to use the APIs which can be a good starting place as well. (My goal is to as be as close to 100% code coverage as possible. Obviously, that will always be a work in progress.) 

The long-term goal is to put the documentation online somewhere (probably as wiki pages hosted in the repository), but I don't have a time frame for when that will be complete. Until then, the source code is fully commented using XML documentation comments to provide IntelliSense support in Visual Studio.

## Bugs and feature requests
Do you have a bug or a feature request? Please use the [issue tracker](https://github.com/scottdorman/cadru/issues) and search for existing and closed issues. If your problem or request isn't addressed yet, go ahead and [open a new issue](https://github.com/scottdorman/cadru/issues/new). 

## Contributing
You can also get involved and [fork the repository](https://github.com/scottdorman/cadru/fork) to submit your own pull requests. (More detailed contributor guidelines will be available soon.)

## Versioning
For transparency and to maintain backward compatibility (as much as possible), Cadru uses the [Semantic Versioning guidelines](http://semver.org/).

## Creators
* [Scott Dorman](http://about.me/scottdorman) ([@sdorman](http://twitter.com/sdorman)) - for the initial conception of the toolkit and core contributor.

## Copyright and license
Code and documentation copyright 2001-2020 Scott Dorman. Code is licensed under the [Microsoft Public License](http://opensource.org/licenses/Ms-PL.html), use it as you wish (but please 
provide some credit somewhere in your app.) Documentation is released under [Creative Commons](https://github.com/scottdorman/cadru/blob/master/docs/LICENSE).
