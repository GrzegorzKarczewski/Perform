using Perform.ViewModels;

namespace Perform;

public partial class MainPage : ContentPage
{
    public string CurrentName = String.Empty;
    private CsvDataViewModel viewModel;
    public MainPage()
    {
        InitializeComponent();
        viewModel = new CsvDataViewModel();

    }


    private async void LoadDataAsync()
    {
        await viewModel.LoadDataAsync();
        LoadDataIntoTable();
    }
    private void LoadDataIntoTable()
    {
        tableView.Root.Clear();
        foreach (var row in viewModel.Rows)
        {
            string currentName = null;
            string colliValue = null;
            string totalcollies = null;

            foreach (var column in row)
            {
                if (column.Key == "Naam_Medewerker" && column.Value.ToUpper() == CurrentName)
                {
                    currentName = $"Name: {column.Value}";
                }
                else if (column.Key == "Colli_Gem_Colli_Per_Uur")
                {
                    colliValue = $"Performance: {column.Value} colli/hr";
                }
                else if (column.Key == "Totaal_Colli_Gedaan")
                {
                    totalcollies = $"You've picked {column.Value} colli ";
                }
            }

            if (currentName != null && colliValue != null && totalcollies != null)
            {
                var tableSection = new TableSection();
                tableSection.Title = "Your latest Performance:";
                tableSection.TextColor = Colors.Black;

                tableSection.Add(new TextCell
                {

                    Text = currentName,
                    TextColor = Colors.Black,


                });
                tableSection.Add(new TextCell
                {
                    Text = colliValue,
                    TextColor = Colors.Black
                });
                tableSection.Add(new TextCell
                {
                    Text = totalcollies.ToString(),
                    TextColor = Colors.Black
                });
                tableView.Root.Add(tableSection);
            }
        }


    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        lblHello.Text = $"Hi {entryName.Text}";
        entryName.IsVisible = false;
        entryName.Unfocus();
        CurrentName = entryName.Text.ToUpper();
        lblEnterLogin.IsVisible = false;
        loadingUserData.IsVisible = true;

        // Load user data
        LoadDataAsync();

        entryName.IsVisible = false;
        tableView.Focus();

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
        // Show the Back button
        btnBack.IsVisible = true;
        btnRefresh.IsVisible = true;

    }

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        loadingUserData.IsVisible = true;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        LoadDataAsync();
    }

    private void btnBack_Clicked(object sender, EventArgs e)
    {
        // Reset your user interface and data here
        // Set the Entry to be visible and clear its text
        entryName.IsVisible = true;
        entryName.Text = string.Empty;

        // Hide lblHello and loadingUserData
        lblHello.IsVisible = false;
        loadingUserData.IsVisible = false;

        // Hide the Refresh button
        btnRefresh.IsVisible = false;

        // Clear the TableView
        tableView.Root.Clear();

        // Show the initial Label texts
        lblHello.Text = "Hello, fellow orderpicker!";
        lblEnterLogin.IsVisible = true;

        // Hide the Back button
        btnBack.IsVisible = false;
    }
}

