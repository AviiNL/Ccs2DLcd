<pre>
________         _____  _____             _________               
\_   ___ \  _____/ ____\/ ____\____   ____ \_   ___ \ __ ________  
/    \  \/ /  _ \   __\\   __\/ __ \_/ __ \/    \  \/|  |  \____ \ 
\     \___(  <_> )  |   |  | \  ___/\  ___/\     \___|  |  /  |_> >
 \______  /\____/|__|   |__|  \___  >\___  >\______  /____/|   __/ 
        \/                        \/     \/        \/      |__|    
  _________ __            .___.__              
 /   _____//  |_ __ __  __| _/|__| ____  ______ ©
 \_____  \\   __\  |  \/ __ | |  |/  _ \/  ___/
 /        \|  | |  |  / /_/ | |  (  <_> )___ \ 
/_______  /|__| |____/\____ | |__|\____/____  >
        \/                 \/               \/ 


A 2D Game Engine for the G19 Keyboard LCD

Project Name: Ccs2DLcd
Project Autor: AviiNL
Project Language: C#
License: MIT
References:	CsLglcd - https://github.com/Fire-Dragon-DoL/CsLglcd
			NAudio  - http://naudio.codeplex.com/
</pre>

# Ccs2DLcd

## Overview

This project is an attempt to render a 2D sprite based game on your G19 Lcd Display.


## Usage

Ccs2DLcd allows you to create your very own game that can be played on the Logitech G19 Keyboard LCD.

### Requirements

 - .NET Framework 4.0
 - Visual Studio C# 2010 Express or above

### Dependencies

 - CsLglcd<br>
 CsLglcd is a .NET wrapper for the G19 SDK
 - NAudio<br>
 NAudio is being used for.. well... audio.

All third party dependencies are included in the project,<br>
so there is no need for you to go look for them ( unless you really really want to.... you can go look them up ;) )

Ccs2DLcd is compiled using Visual Studio 2010 Express for C# with the .NET Framework 4.0.<br>

### Getting started

To use Ccs2DLcd open Visual Studio and create a new Console Application in C#<br>
You can use a windows form, but in my experience it will stay empty anyway.

Once in your project add reference to Ccs2DLcd.dll or use the project.<br>
Build it once to create output files and copy the dll.<br>
Navigate to [your project]\bin\Debug\ and copy the other dll's from [Ccs2DLcd]\bin\Debug\ or [Ccs2DLcd]\bin\Release\

Add ' using Ccs2DLcd; ' to the top of your Program.cs (or Form1.cs if using windows forms application)<br>
Now you are ready to make your own game.


## Copyright and License

Copyright &copy; 2013 CoffeeCup Studios

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

### Sidenote

All resources used in the TestCase are grabbed from google image search,<br>
The files are not owned by me in any way and are property of their respective owners.