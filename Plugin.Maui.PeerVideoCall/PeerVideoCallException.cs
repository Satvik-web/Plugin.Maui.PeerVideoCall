namespace Plugin.Maui.PeerVideoCall
{
    public class PeerVideoCallException : Exception
    {
        public PeerVideoCallException() { }

        public PeerVideoCallException(string message)
            : base(message) { }

        public PeerVideoCallException(string message, Exception inner)
            : base(message, inner) { }
    }
}