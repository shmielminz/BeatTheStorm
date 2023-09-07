namespace BeatTheStormMAUI;

public partial class Help : ContentPage
{
	public Help()
	{
		InitializeComponent();
	}

    private void CloseBtn_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }
}