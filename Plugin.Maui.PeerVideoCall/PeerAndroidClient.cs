#if ANDROID
using Android.Webkit;

namespace Plugin.Maui.PeerVideoCall;

internal sealed class PeerAndroidClient : WebChromeClient
{
    public override async void OnPermissionRequest(PermissionRequest request)
    {
        try
        {
            var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var micStatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();

            if (cameraStatus == PermissionStatus.Granted &&
                micStatus == PermissionStatus.Granted)
            {
                request.Grant(request.GetResources());
                return;
            }

            throw new Exception("Camera And Mic Permission Required");
        }
        catch (Exception ex)
        {
            throw new PeerVideoCallException($"WebView Permission Setter Error {ex.Message}");
        }
    }
}
#endif