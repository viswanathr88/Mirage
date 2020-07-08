Mirage
======
[NuGet](https://www.nuget.org/packages/Mirage.Mvvm/)

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
 
Design
------
 
The library is a Universal Windows Class Library. The minimum SDK version is Win10 10240.

License
-------

Mirage is an open source library with a GPL license.
