#if WINDOWS
using Microsoft.Web.WebView2.Core;
using Microsoft.Maui.Handlers;

namespace Plugin.Maui.PeerVideoCall;

internal static class PeerWindowsClient
{
    public static async Task ApplyPermissions(IWebViewHandler handler)
    {
        try 
	    {	        
		var nativeWebView = (Microsoft.UI.Xaml.Controls.WebView2)handler.PlatformView;

        // Force engine initialization NOW [Same as your test project]
        await nativeWebView.EnsureCoreWebView2Async();

        // Setup permissions
        nativeWebView.CoreWebView2.PermissionRequested += (sender, args) =>
        {
            if (args.PermissionKind == CoreWebView2PermissionKind.Microphone || 
                args.PermissionKind == CoreWebView2PermissionKind.Camera)
            {
                args.State = CoreWebView2PermissionState.Allow;
                args.Handled = true;
            }
        };
	    }
	    catch (Exception ex)
	    {
		    throw new PeerVideoCallException($"WebView Permission Setter Error {ex.Message}");
	    }
    }
}
#endif