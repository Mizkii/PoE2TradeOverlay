# PoE2 Trade Overlay

Lightweight overlay application for Path of Exile 2 trading

## Features

- Toggle overlay with customizable hotkey (default: F1)
- Click-through mode when hidden
- Position/size memory between sessions
- Minimal UI with draggable titlebar
- Persistent configuration

## Technical Details

- Platform: Windows
- Framework: .NET 8.0
- Dependencies: Microsoft.Web.WebView2


## Installation

1. Install .NET 8.0 SDK
2. Install required packages:
dotnet add package Microsoft.Web.WebView2


## Usage:
Build the application

Start the Game and set it to Windowed Fullscreen

Run the application

Press F1 to toggle overlay visibility

Use titlebar to:

Change hotkey

Refresh page

Minimize/close


Technical details:
- Configuration auto-saves on close
- Full WebView2 process cleanup
- Native Windows API integration for window manipulation
- Event-driven architecture for state management
