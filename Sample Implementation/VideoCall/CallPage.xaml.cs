namespace VideoCall;

public partial class CallPage : ContentPage
{
	public CallPage()
	{
		InitializeComponent();
	}

    private void Initialize_Btn_Clicked(object sender, EventArgs e)
    {
        VideoCallView.Initialise();
    }

    private void AddPeer_Btn_Clicked(object sender, EventArgs e)
    {
        VideoCallView.AddConnectionIDToCall(PeerID_Entry.Text.Trim());
    }

    private async void GetID_Btn_Clicked(object sender, EventArgs e)
    {
        string ID = await VideoCallView.GetConnectionID();
        MyID_Label.Text = $"My Peer ID: {ID}";
    }
}