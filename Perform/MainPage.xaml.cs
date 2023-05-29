using Perform.ViewModels;

namespace Perform;

public partial class MainPage : ContentPage
{
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
        foreach (var row in viewModel.Rows)
        {
            var tableSection = new TableSection();
            tableSection.Title = "Your Performace:";
            tableSection.TextColor = Colors.Black;
            foreach (var column in row)
            {
                if (column.Key == "Naam_Medewerker" || column.Key == "Colli_Gem_Colli_Per_Uur")
                {
                    if (column.Value.ToUpper() == entryName.ToString())
                    {
                        tableSection.Add(new TextCell
                        {
                            Text = $"{column.Key}: {column.Value}",
                            TextColor = Colors.Black

                        });
                    }
                }
            }

            tableView.Root.Add(tableSection);
        }

    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        lblHello.Text = $"Hi {entryName.Text}";
        entryName.IsVisible = false;
        entryName.Text.ToUpper();
        lblEnterLogin.IsVisible = false;

        LoadDataAsync();
        // loadingUserData.IsVisible = false;
    }

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        // loadingUserData.IsVisible = true;
    }
}

