#if IOS || MACCATALYST
using WebKit;
using Foundation;
using UIKit;

namespace Plugin.Maui.PeerVideoCall;

internal sealed class PeerIOSClient : WKUIDelegate
{
    [Export("webView:requestMediaCapturePermissionForOrigin:initiatedByFrame:type:decisionHandler:")]
    public async override void RequestMediaCapturePermission(
        WKWebView webView, 
        WKSecurityOrigin origin, 
        WKFrameInfo frame, 
        WKMediaCaptureType type, 
        Action<WKPermissionDecision> decisionHandler)
    {
        try 
	    {	        
		    var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var micStatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();

            if (cameraStatus == PermissionStatus.Granted &&
                micStatus == PermissionStatus.Granted)
            {
                decisionHandler(WKPermissionDecision.Grant);
                return;
            }

            decisionHandler(WKPermissionDecision.Deny);

            throw new Exception("Camera And Mic Permission Required");
	    }
	    catch (Exception ex)
	    {
            throw new PeerVideoCallException($"WebView Permission Setter Error {ex.Message}");
	    }
    }
}
#endif