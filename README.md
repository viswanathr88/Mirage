Mirage
======

[![Build Status](https://dev.azure.com/viswanath/Mirage.Mvvm/_apis/build/status/viswanathr88.Mirage?branchName=master)](https://dev.azure.com/viswanath/Mirage.Mvvm/_build/latest?definitionId=11&branchName=master)

![NuGet](https://img.shields.io/nuget/v/Mirage.Mvvm.svg)

Mirage is a windows library that provides some common classes to write better Model-View-ViewModel (MVVM) applications. Mirage provides a command framework, a ViewModel framework and a bunch of collection classes that make it easy to write ViewModels for windows applications. 

Read more about MVVM [here](https://msdn.microsoft.com/en-us/library/hh848246.aspx).

Who is the target audience?
---------------------------

Developers who want to develop for Windows can use this library to extend their viewmodels. This library is for Universal Windows Platform Apps (UWP Apps) running on all Windows 10 devices, Xbox One consoles, Holo Lens etc.

Features
--------

The following are some of the features of this library:
 * A Command Framework that supports type safe parameters, type safe results, cancellation, state events etc.
 * A ViewModelBase class that provides ability to set a member of the class so that property change events are raised automatically, raise property changed events without having to hardcode the name of the property etc.
 * A DataViewModel class that can be used for ViewModels that load any kind of data and update properties. This also comes with an easy way to register and de-register commands that are part of the ViewModel.
 * Some collection classes like SortedList that are not part of the standard APIs provided by Microsoft.
 
How to use?
----------

- Derive all ViewModel classes that are tied to a page from DataViewModel or DataViewModel<T> depending on what parameter is required to hydrate the page
- Derive all synchronous commands from the Command class
- Derive all asynchronous commands from the AsyncCommand class
 
Design
------
 
The library is a Universal Windows Class Library. The minimum SDK version is Win10 10240.

License
-------

Mirage is an open source library with a GPL license.
