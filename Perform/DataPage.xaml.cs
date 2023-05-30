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


    private void Button_Clicked(object sender, EventArgs e)
    {
        LoadDataAsync();
    }

}