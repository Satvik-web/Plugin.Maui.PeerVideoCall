using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Plugin.Maui.PeerVideoCall;

public partial class PeerVideoCallView : ContentView
{
    /// <summary>
    /// The WebRTC Engine that's used to make video calls. It's a Webview. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public WebView WebRTCView;

    public PeerVideoCallView()
    {
        InitializeComponent();

        Task.Run(() => LoadHtml());
    }

    private async Task LoadHtml()
    {
        try
        {
            PeerVideoCallWebViewMapper.EnsureMapped();

#if WINDOWS
            if (Web.Handler is IWebViewHandler handler)
            {
                await PeerWindowsClient.ApplyPermissions(handler);
            }
#endif
            Web.Source = new UrlWebViewSource
            {
                Url = "https://satvik-web.github.io/Plugin.Maui.PeerVideoCall/index.html"
            };

            WebRTCView = Web;
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"View Load Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Initializes the PeerJS engine with default configuration.. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task Initialise()
    {
        try
        {
            await Web.EvaluateJavaScriptAsync("window.Initialise();").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"Initialization Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Initializes the PeerJS engine with a custom PeerJS configuration.. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <param name="PeerConfig">An object containing PeerJS configuration options. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    public async Task Initialise(object PeerConfig)
    {
        try
        {
            await Web.EvaluateJavaScriptAsync($"window.Initialise({JsonSerializer.Serialize(PeerConfig)});").ConfigureAwait(false);

        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"Initialization Error. {ex.Message}");
        }    
    }

    /// <summary>
    /// Starts a video call to a specific Peer ID. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <param name="PartnerId">The unique Connection ID of the remote peer. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    public async Task AddConnectionIDToCall(string PartnerId)
    {
        try
        {
            await Web.EvaluateJavaScriptAsync($"window.AddConnectionIDToCall({JsonSerializer.Serialize(PartnerId)});").ConfigureAwait(false);

        }
        catch (Exception ex)
        {

            throw new PeerVideoCallException($"AddConnectionIDToCall Error. {ex.Message}");
        }    
    }

    /// <summary>
    /// Ends all active calls and clears the video grid. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task DropCall()
    {
        try
        {
            await Web.EvaluateJavaScriptAsync("window.DropCall();").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"DropCall Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Disables the UI and displays a "Switched Off" state. Useful for clearing resources without disposing the view. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task SwitchOff()
    {
        try
        {
            await Web.EvaluateJavaScriptAsync("window.SwitchOff();").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"SwitchOff Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Reloads the peerjs engine to reset the view state to "On". See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task SwitchOn()
    {
        try
        {
            await Web.EvaluateJavaScriptAsync("window.SwitchOn();").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"SwitchOn Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Toggles the camera - on or off. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task ToggleVideo()
    {
        try
        {
            await Web.EvaluateJavaScriptAsync("window.ClickVideo();").ConfigureAwait(false);

        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"ToggleVideo Error. {ex.Message}");
        }    
    }

    /// <summary>
    /// Toggles the mic - on or off. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task ToggleAudio()
    {
        try
        {
            await Web.EvaluateJavaScriptAsync("window.ClickMic();").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"ToggleAudio Error. {ex.Message}");
        }
    }


    /// <summary>
    /// Executes a custom JavaScript function within the context. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <param name="JavaScriptFunction">The name of the function to call (without parentheses). See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    /// <returns>The result returned from the JavaScript execution. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<string> RunJavaScript(string JavaScriptFunction)
    {
        try
        {
            return await Web.EvaluateJavaScriptAsync($"{JavaScriptFunction.Trim()}();").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"Run JavaScript Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Executes a custom JavaScript function within the context. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <param name="JavaScriptFunction">The name of the function to call (without parentheses). See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    /// <param name="Params">The Parameters that are to be sent to the function to execute. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    /// <returns>The result returned from the JavaScript execution. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<string> RunJavaScript(string JavaScriptFunction, object Params)
    {
        try
        {
            return await Web.EvaluateJavaScriptAsync($"{JavaScriptFunction}({JsonSerializer.Serialize(Params)});").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"Run JavaScript Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the display name of a peer id in the video grid. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <param name="name">The name that is to be set for that peer. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    /// <param name="peerId">The unique Connection ID of the peer. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</param>
    public async Task SetName(string name, string peerId)
    {
        try
        {
            var safeName = JsonSerializer.Serialize(Regex.Unescape(name));
            var safeId = JsonSerializer.Serialize(Regex.Unescape(peerId));

            await Web.EvaluateJavaScriptAsync($"window.SetName({safeName}, {safeId});").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"SetName Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Injects a raw CSS string to customize the look of the video grid or buttons. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task InjectCSS(string css)
    {
        try
        {
            await Web.EvaluateJavaScriptAsync($"window.InjectCSS({JsonSerializer.Serialize(css)});").ConfigureAwait(false);

        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"InjectCSS Error. {ex.Message}");
        }    
    }

    /// <summary>
    /// Injects a javascript string containing custom code to alter how the view functions. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    public async Task InjectJavaScript(string js)
    {
        try
        {
            await Web.EvaluateJavaScriptAsync($"window.InjectJavaScript({JsonSerializer.Serialize(js)});").ConfigureAwait(false);

        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"InjectJavaScript Error. {ex.Message}");
        }    
    }

    /// <summary>
    /// Retrieves the unique Connection ID for the local peer assigned by the signaling server. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <returns>The Peer ID string, or an empty string if not yet connected. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<string> GetConnectionID()
    {
        try
        {
            string result = await Web.EvaluateJavaScriptAsync("window.GetConnectionID();").ConfigureAwait(false);
            if (string.IsNullOrEmpty(result) || result == "null") return "";

            return result.Trim('"');
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"GetConnectionID Error. {ex.Message}");
        }
    }


    /// <summary>
    /// Checks if there is at least one active remote peer connection. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <returns>True if a call is active; otherwise false. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<bool> IsCallConnected()
    {
        try
        {
            string result = await Web.EvaluateJavaScriptAsync("window.IsCallConnected();").ConfigureAwait(false);
            return result?.ToLower() == "true";
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"IsCallConnected Check Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a list of all Peer IDs currently participating in the call. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <returns>A list of strings containing Peer IDs. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<List<string>> GetPeerIDsInCall()
    {
        try
        {
            string jsonResult = await Web.EvaluateJavaScriptAsync("window.GetPeerIDsInCall();").ConfigureAwait(false);

            jsonResult = Regex.Unescape(jsonResult);

            if (string.IsNullOrEmpty(jsonResult) || jsonResult == "[]" || jsonResult == "null")
            {
                return new List<string>();
            }

            return JsonSerializer.Deserialize<List<string>>(jsonResult);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"GetPeerIDsInCall Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves the internal logs from the WebRTC engine for debugging purposes. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <returns>A list of log entries with timestamps. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<List<string>> RetrieveLogs()
    {
        try
        {
            string jsonResult = await Web.EvaluateJavaScriptAsync("window.RetrieveLogs();").ConfigureAwait(false);

            jsonResult = Regex.Unescape(jsonResult);

            if (string.IsNullOrEmpty(jsonResult) || jsonResult == "[]" || jsonResult == "null")
            {
                return new List<string>();
            }

            return JsonSerializer.Deserialize<List<string>>(jsonResult);
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"RetrieveLogs Error. {ex.Message}");
        }
    }

    /// <summary>
    /// Checks if the view is turned off. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall
    /// </summary>
    /// <returns>True if the view is turned off; otherwise false. See Documentation https://github.com/Satvik-web/Plugin.Maui.PeerVideoCall</returns>
    public async Task<bool> IsSwitchedOff()
    {
        try
        {
            string result = await Web.EvaluateJavaScriptAsync("window.IsSwitchedOff();").ConfigureAwait(false);
            return result?.ToLower() == "true";
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"IsSwitchedOff Check Error. {ex.Message}");
        }
    }
}