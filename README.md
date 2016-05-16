# UnityRapidXml
this is a simple wrapper for rapidxml(http://rapidxml.sourceforge.net/) for C#.
it is the fastest xml parser in C#, i crate this library because the default xml parser in c# is too swollen and slow.
in Unity Engine, there is a Mono.xml you can use. but is also slow. 
this library is implemented in native, and easy use in c#.
you can clone or download this project to see the detail.
i have test it on windows,android and ios. you can use it for your unity game.

tutorial:
   please install visiual studio 2015 and mobile developer kit, and then you can open the solution file directly. or you can create the project by yourself. all of the codes are under the directory RapidXml, all export functions in Source/RapidXml.cpp
   
   you can build this cpp file to dll for windows, copy the dll to Assets/Plugins/X86
   build android so file, copy to Assets/Plugins/Android.
   build bundle for macosx, copy to Assets/Plugins
   ios: copy code files to your xcode project if you use il2cpp, you can use XUPorter to do this.   

bodong
Email: bodong@tencent.com

