namespace VideoCall
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Chk_Permit_Clicked(object sender, EventArgs e)
        {
            var camstatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var micstatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();

            if (camstatus != PermissionStatus.Granted || micstatus != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();
                await Permissions.RequestAsync<Permissions.Microphone>();

                var newcamstatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
                var newmicstatus = await Permissions.CheckStatusAsync<Permissions.Microphone>();

                if (newcamstatus != PermissionStatus.Granted || newmicstatus != PermissionStatus.Granted)
                {
                    Status_Label.Text = "Permissions not granted...";
                }
                else 
                {
                    Status_Label.Text = "Permissions granted...";
                    Caller_Btn.IsEnabled = true;
                }
            }
            else 
            {
                Status_Label.Text = "Permissions already granted...";
                Caller_Btn.IsEnabled = true;
            }
        }

        private void Caller_Btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CallPage());
        }
    }
}
