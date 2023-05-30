namespace Perform;
using ViewModels;

public partial class DataPage : ContentPage
{

    public string CurrentName = String.Empty;
    private CsvDataViewModel viewModel;

    public DataPage(string currentName)
    {
        InitializeComponent();
        CurrentName = currentName;
        viewModel = new CsvDataViewModel();
        LoadDataAsync();
    }
    private async void LoadDataAsync()
    {
        await viewModel.LoadDataAsync();

        if (IsUserFound())
        {
            LoadDataIntoTable();
        }
        else
        {
            await DisplayAlert("User not found", $"Name {CurrentName} couldn't be found in the system. Contact teamleader for your name", "OK");
            await Navigation.PopAsync();
        }
    }
    private void LoadDataIntoTable()
    {
        tableView.Root.Clear();

        // Iterate over each row in the data
        foreach (var row in viewModel.Rows)
        {
            string currentName = null;
            string colliValue = null;
            string totalcollies = null;
            string lastOrder = null;

            // Iterate over each column in the row
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
                else if (column.Key == "Tijd_Laatst_Gedane_Taak")
                {
                    lastOrder = column.Value;
                }
            }
            // If all necessary data is found, create a new TableSection and add it to the table
            if (currentName != null && colliValue != null && totalcollies != null && lastOrder != null)
            {
                var tableSection = new TableSection();
                tableSection.Title = $"Your latest Performance: {lastOrder}";
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

        btnRefresh.IsVisible = true;
    }
    private bool IsUserFound()
    {
        foreach (var row in viewModel.Rows)
        {
            foreach (var column in row)
            {
                if (column.Key == "Naam_Medewerker" && column.Value.ToUpper() == CurrentName)
                {
                    return true;
                }
            }
        }

        return false;
    }


    private void ButtonRefresh_Clicked(object sender, EventArgs e)
    {
        LoadDataAsync();
    }

}