namespace Perform;

public partial class MainPage : ContentPage
{
    public string CurrentName = String.Empty;
    public MainPage()
    {
        InitializeComponent();
    }

    private void Entry_Completed(object sender, EventArgs e)
    {


        CurrentName = entryName.Text.ToUpper();
        // Navigation occurs here, passing the name to the second page
        Navigation.PushAsync(new DataPage(CurrentName));



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


    private void btnBack_Clicked(object sender, EventArgs e)
    {
        //// Reset your user interface and data here
        //// Set the Entry to be visible and clear its text
        //entryName.IsVisible = true;
        //entryName.Text = string.Empty;

        //// Hide lblHello and loadingUserData
        //lblHello.IsVisible = false;
        //loadingUserData.IsVisible = false;

        //// Hide the Refresh button
        //// btnRefresh.IsVisible = false;

        //// Clear the TableView
        //// tableView.Root.Clear();

        //// Show the initial Label texts
        //lblHello.Text = "Hello, fellow orderpicker!";
        //lblEnterLogin.IsVisible = true;

        //// Hide the Back button
        //btnBack.IsVisible = false;
    }
}

