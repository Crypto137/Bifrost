# Bifrost

Bifrost is a custom launcher for Marvel Heroes. 

<img src="./Bifrost.Wpf/Screenshot.png" title="" alt="Screenshot" data-align="center">

## Features

* Managing settings for connecting to different servers.

* Forcing custom resolution not available via in-game options, such as 4K or ultrawide.

* Skipping startup logo movies.

* Auto-login for playing on a local server.

* Client logging configuration.

* Game version detection.

## Installation

1. Install [.NET Desktop Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.33-windows-x64-installer) if you do not have it installed already.

2. Download the latest release [here](https://github.com/Crypto137/Bifrost/releases).

3. Copy Bifrost.exe to your Marvel Heroes installation directory.

4. Run Bifrost.exe.

**NOTE: Bifrost is an unsigned executable that starts another process. This may cause false positive detections from various antivirus and antimalware software. If this causes inconvenience, please feel free to build the source code yourself.**

## Additional Details

Bifrost consists of three main parts:

- `Bifrost.Launcher` - a backend library.

- `Bifrost.ConsoleApp` - a very simple example program that demonstrates how to use Bifrost.Launcher.

- `Bifrost.Wpf` - a WPF-based graphical interface that uses Bifrost.Launcher.

Bifrost.Launcher and Bifrost.ConsoleApp are cross-platform and should be able to target any .NET supported platform. Bifrost.Wpf relies on WPF and requires Windows or compatible environment.
