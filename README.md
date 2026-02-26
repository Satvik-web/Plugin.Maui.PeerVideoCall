# Plugin.Maui.PeerVideoCall

A lightweight, high-performance .NET MAUI Package for Peer-to-Peer (P2P) video calling. This plugin wraps WebRTC and PeerJS into a simple ContentView that you can drop into any MAUI XAML page.

The Packgage uses the index.html from this repository for Video Calling

Install from: https://www.nuget.org/packages/Plugin.Maui.PeerVideoCall/

# Setup Permissions

Android: 
Add the following lines to your AndroidManifest.xml

```xml
<uses-feature android:name="android.hardware.camera"/>
<uses-feature android:name="android.hardware.camera.autofocus"/>

<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.RECORD_AUDIO" />
<uses-permission android:name="android.permission.MODIFY_AUDIO_SETTINGS" />
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.READ_MEDIA_IMAGES" />
```

iOS:
Add the following lines to your Info.plist

```xml
<key>NSCameraUsageDescription</key>
<string>This app needs access to the camera for video calling.</string>
<key>NSMicrophoneUsageDescription</key>
<string>This app needs access to the microphone for audio calling.</string>
```

# Prerequisites
1. Add the namespace to your xaml page.
```xaml
xmlns:rtc="clr-namespace:Plugin.Maui.PeerVideoCall;assembly=Plugin.Maui.PeerVideoCall"
```

2. Add the following View in your page.
```xaml
<rtc:PeerVideoCallView x:Name="VideoCallView" 
                       HorizontalOptions="FillAndExpand" 
                       VerticalOptions="FillAndExpand" />
```

# Initialization

To Initialize the Video Call View using default PeerJS Options (Using the public PeerJS servers).
```csharp
await VideoCallView.Initialise();
```

To Use your private PeerJS options.

```csharp
object Config = new
{
    host = "Custom PeerJS server url",
    secure = true,
    config = new {
        iceServers = new[] {
            new { urls = "Custom STUN server url" },
            new { 
                urls = "", 
                username = "", 
                credential = "" 
            }
        }
    }
};

await VideoCallView.Initialise(Config);
```
# Get Your Connection ID.
Connection ID is the ID that your device uses to identify among the PeerJS pool, this ID is unique for each session and is used to start peer to peer calls.
```csharp
await VideoCallView.GetConnectionID();
```

# Add Peers to Call
Peers can be added to the existing call using their respective Connection IDs.
```csharp
await VideoCallView.AddConnectionIDToCall("friend-peer-connection-id");
```
Please note that while you can add a lot of peers to a single call, it's recommended to limit to 3 or 4 since multi peer calls slows down the devices. 

# Setting Names for Peers in a Call
By Default the names shown under each connected peers display their connection ID. 
This could be changed by the following function. 

```csharp
await VideoCallView.SetName("peer name", "connection id of that peer");
```

# Controlling Video And Audio
The Camera And Mic can be contolled by the following function. If it's turn off, it gets turned on and vice-versa.

```csharp
await VideoCallView.ToggleVideo();

await VideoCallView.ToggleAudio();
```

# Getting Peers In a Call
You can get the Peer Connection IDs in your call as a List<string>
```csharp
List<string> participants = await VideoCallView.GetPeerIDsInCall();
```

# Leaving the Call
The call shall be terminated by the following.
```csharp
await VideoCallView.DropCall();
```

# Injecting CSS
Since the Views are based on Webview you can modify the styles using the following. Refer to index.html for more context.

```csharp
string customStyle = @"
    video { 
        border: 3px solid #512BD4; 
        border-radius: 15px; 
        background-color: #222;
    }
    .peer-name-label { 
        color: white; 
        background: rgba(0,0,0,0.6); 
        padding: 5px 10px; 
        font-family: 'Segoe UI', sans-serif;
    }";

await VideoCallView.InjectCSS(customStyle);
```

# Injecting JavaScript
Similarly Custom Java Script Functions can be inserted

```csharp
string customFunction = @"
    window.SetBorderColor = function(data)
    {
      const video = document.getElementById(data.id);
      if (video) {
          video.style.borderColor = data.color;
      }
      return "Some stuff";
    };
";

await VideoCallView.InjectJavaScript(customFunction);
```

Note: The function will not run by default it must be triggered. 

# Running Custom JavaScript Functions 
You can run your already inserted custom JavaScript functions and/or existing functions using the following function.
```csharp
var settings = new { 
    id = "remote-peer-123", 
    color = "red" 
};

string ReturnedValue = await VideoCallView.RunJavaScript("SetBorderColor", settings); // ReturnedValue will be set as "Some Stuff".
```

Funtions which doesn't require any parameters can be run by the following.
```csharp
await VideoCallView.RunJavaScript("SomeFunction");
```
Note: Do not include paranthesis. And all parameters should be passed through in a similar manner as the given example, else it might fail.
# Turning the View Off
To safetly turn the full view off without disposing it the following can be used.

```csharp
await VideoCallView.SwitchOff();
```

It can be Turned Back On by:
```csharp
await VideoCallView.SwitchOn();
```

To Check its status:
```csharp
bool status = await VideoCallView.IsSwitchedOff();
```

# Getting Logs
You can obtain a debug log as a List<string>
```csharp
List<string> Logs = await VideoCallView.RetrieveLogs();
```

# Checking if Peers are there in a Call.
You can check if there are other peers except you in a call.
```csharp
bool ArePeersThere = await VideoCallView.IsCallConnected();
```

# Limitations
1. For Android the minimum sdk is to be set to API 23 - Android 6
2. For iOS the View asks for Camera and Mic permissions everytime its loaded, this is a limitation due to Apple's WebView Policies. Future versions of this package will use native webrtc libraries for iOS to stop this behaviour. But for now, this limitation persists.
