namespace Perform;

public partial class MainPage : ContentPage
{
    public string CurrentName = String.Empty;
    public bool beenBack = false;
    NetworkAccess accessType = Connectivity.Current.NetworkAccess;

    public MainPage()
    {
        InitializeComponent();
    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        loadingUserData.IsRunning = false;
        loadingUserData.IsVisible = false;
        if (entryName.Text != null)
        {
            CurrentName = entryName.Text.ToUpper();
        }
        // Navigation occurs here, passing the name to the second page
        if (CurrentName != String.Empty)
        {

            if (accessType == NetworkAccess.Internet)
            {
                // Connection to internet is available
                lblEmptyField.IsVisible = false;

                Navigation.PushAsync(new DataPage(CurrentName));
                beenBack = true;
            }
            else
            {
                DisplayAlert("No internet Access", "Turn on your internet acess", "Got it!");
            }
        }
        else
        {
            DisplayAlert("Login", "Login field is empty, please input your workers login", "Got it!");
            lblEmptyField.IsVisible = true;
        }
#if ANDROID
                var imm = (Android.Views.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(Android.Content.Context.InputMethodService);

                if (imm != null)
                {
                    //this stuff came from here: 
                    var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
                    Android.OS.IBinder wToken = activity.CurrentFocus?.WindowToken;
                    imm.HideSoftInputFromWindow(wToken, 0);
                }
#endif

    }

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (entryName.Text.Length != 0)
        {
            lblEmptyField.IsVisible = false;
            loadingUserData.IsVisible = true;
            loadingUserData.IsRunning = true;
            lblSearching.IsVisible = true;
        }
        else
        {
            loadingUserData.IsVisible = false;
            loadingUserData.IsRunning = false;
            lblSearching.IsVisible = false;
        }

    }

    protected override async void OnAppearing()
    {
        // piggybacking this animation function to solve problem 
        // TODO: make it smarter in the future
        if (beenBack)
        {
            {
                entryName.Text = string.Empty;
                loadingUserData.IsVisible = false;
                loadingUserData.IsRunning = false;
                lblSearching.IsVisible = false;
            }
        }
        //---------------------------------------------------------
        // Animation part
        base.OnAppearing();

        // Initial state (invisible)
        lblHello.Opacity = 0;

        // Animation (fade in)
        await lblHello.FadeTo(1, 2000); // parameters: target opacity, duration (milliseconds)

    }

}

