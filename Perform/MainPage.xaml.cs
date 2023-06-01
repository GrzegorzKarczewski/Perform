namespace Perform;

public partial class MainPage : ContentPage
{
    public string CurrentName = String.Empty;
    NetworkAccess accessType = Connectivity.Current.NetworkAccess;

    public MainPage()
    {
        InitializeComponent();
    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        CurrentName = entryName.Text.ToUpper();

        // Navigation occurs here, passing the name to the second page
        if (accessType == NetworkAccess.Internet)
        {
            // Connection to internet is available
            Navigation.PushAsync(new DataPage(CurrentName));
        }
        else
        {
            DisplayAlert("No internet Access", "Turn on your internet acess", "Got it!");
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
        loadingUserData.IsVisible = true;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Initial state (invisible)
        lblHello.Opacity = 0;

        // Animation (fade in)
        await lblHello.FadeTo(1, 2000); // parameters: target opacity, duration (milliseconds)
    }

}

