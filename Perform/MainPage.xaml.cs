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
            }
            else
            {
                DisplayAlert("No internet Access", "Turn on your internet acess", "Got it!");
            }
        }
        else
        {
            // If string is empty place a warning message
            // SoftBlink(lblEmptyField, Color.FromArgb("1E1E1E"), Colors.Red, 2000, false);
            // SoftBlink(lblEmptyField, Color.FromArgb("1E1E1E"), Colors.Green, 2000, true);
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
        if (entryName.Text != null)
        {
            lblEmptyField.IsVisible = false;
            loadingUserData.IsVisible = true;
            loadingUserData.IsRunning = true;
        }
        else
        {
            loadingUserData.IsVisible = false;
            loadingUserData.IsRunning = false;
        }

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Initial state (invisible)
        lblHello.Opacity = 0;

        // Animation (fade in)
        await lblHello.FadeTo(1, 2000); // parameters: target opacity, duration (milliseconds)
    }

    //private static async Task SoftBlink(View ctrl, Color c1, Color c2, short CycleTime_ms, bool BkClr)
    //{
    //    var sw = new Stopwatch(); sw.Start();
    //    short halfCycle = (short)Math.Round(CycleTime_ms * 0.5);
    //    while (true)
    //    {
    //        await Task.Delay(1);
    //        var n = sw.ElapsedMilliseconds % CycleTime_ms;
    //        var per = Math.Abs(n - halfCycle) / (double)halfCycle;
    //        var red = (c2.Red - c1.Red) * per + c1.Red;
    //        var grn = (c2.Green - c1.Green) * per + c1.Green;
    //        var blw = (c2.Blue - c1.Blue) * per + c1.Blue;
    //        var clr = Color.FromRgb(red, grn, blw);
    //        if (BkClr)
    //        {
    //            ctrl.BackgroundColor = clr;
    //        }
    //        else if (ctrl is Label label)
    //        {
    //            label.TextColor = clr;
    //        }
    //    }
    //}



}

