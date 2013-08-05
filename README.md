# Cadru

## What is Cadru?

Cadru is (hopefully) a helpful utility framework containing new APIs and extensions to the core .NET Framework
to help complete your developer toolbox. Cadru is designed to be portable first, which means that the majority
of the library is available as a Portable Class Library (PCL) targeting the following frameworks:

* .NET Framework 4 and higher
* Silverlight 4 and higher
* Windows Phone 7 and higher
* .NET for Windows Store apps

This set of target frameworks was chosen to provide Cadru with the broadest reach possible. Any platform specific
functionality is exposed as more narrowly focused portable class libraries or as a non portable library if that’s
the only option.

The docs for Cadru are always evolving to be better. The wiki docs (coming soon) are the permanent place where
the docs will continue to get better on specific use cases of the library. There are also a lot of unit tests that
show how to use the APIs and can be a good starting place as the docs continue to evolve. (My goal is to as be as
close to 100% code coverage as possible. Obviously, that will always be a work in progress.)

Cadru is provided as open source licensed under the Microsoft Public License, use it as you wish (but please 
provide some credit somewhere in your app.)

## What's in it?

So far, Cadru includes:

* **Cadru.Core** - A portable class library which provides the majority of the framework.
* **Cadru.Core.Windows** - A non-portable class library (targeting .NET Framework 4) meant for Windows desktop applications.
* **Cadru.UnitTest.Framework** - A non-portable class library (targeting .NET Framework 3.5) which adds additional assert capabilities for MSTest.

## How do I get Cadru?

I’ve tried to make it easy to get Cadru for your own use. Right now, it’s available in source code form on 
GitHub but I’m working on making it available in binary form through NuGet and the Visual Studio Gallery.

## I found an issue

Great, please [log a bug](https://github.com/scottdorman/cadru/issues/new) so that it can be tracked.

## Credits and Acknowledgements
* [Scott Dorman](http://about.me/scottdorman) ([@sdorman](http://twitter.com/sdorman)) - for the initial conception of the toolkit and core contributor.
