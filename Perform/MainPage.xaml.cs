using Perform.ViewModels;

namespace Perform;

public partial class MainPage : ContentPage
{
    private CsvDataViewModel viewModel;
    public MainPage()
    {
        InitializeComponent();
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
        foreach (var row in viewModel.Rows)
        {
            var tableSection = new TableSection();
            foreach (var column in row)
            {
                tableSection.Add(new TextCell { Text = $"{column.Key}: {column.Value}" });
            }

            tableView.Root.Add(tableSection);
        }
    }

}

