namespace Plugin.Maui.PeerVideoCall;

internal static class PeerVideoCallWebViewMapper
{
    static bool _mapped;

    internal static void EnsureMapped()
    {
        try
        {
            if (_mapped)
                return;

            _mapped = true;

            Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("PeerVideoCallPermissions", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Settings.JavaScriptEnabled = true;
                handler.PlatformView.Settings.MediaPlaybackRequiresUserGesture = false;
                handler.PlatformView.Settings.DomStorageEnabled = true;
                handler.PlatformView.SetWebChromeClient(new PeerAndroidClient());
#elif IOS || MACCATALYST
            handler.PlatformView.UIDelegate = new PeerIOSClient();
            handler.PlatformView.Configuration.AllowsInlineMediaPlayback = true; 
            handler.PlatformView.Configuration.MediaTypesRequiringUserActionForPlayback = WebKit.WKAudiovisualMediaTypes.None;
            handler.PlatformView.Configuration.Preferences.JavaScriptCanOpenWindowsAutomatically = true;
#endif
            });
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"WebView Mapping Error. {ex.Message}");
        }
    }
}
