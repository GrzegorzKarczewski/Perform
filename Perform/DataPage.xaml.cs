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
        AI_tableNotLoaded.IsRunning = true;
        AI_tableNotLoaded.IsVisible = true;
        lblLoading.IsVisible = true;
        await viewModel.LoadDataAsync();

        if (IsUserFound())
        {
            // Here we are checking if worker had norm higher or equal than expected
            int score = LoadDataIntoTable();
            //await DisplayAlert("Performance", $"Your performance is : {score}", "OK");
            if (score >= 160)
            {
                lblGoodJob.IsVisible = true;
                lblShy.IsVisible = false;
                frameSummary.BackgroundColor = Color.FromArgb("0ef1c2");
            }
            else
            {
                lblCanDoBetter.IsVisible = true;
                lblShy.IsVisible = true;

            }
        }
        else
        {
            await DisplayAlert("User not found", $"Name {CurrentName} couldn't be found in the system. Contact teamleader for your name", "OK");
            await Navigation.PopAsync();
        }
        AI_tableNotLoaded.IsRunning = false;
        AI_tableNotLoaded.IsVisible = false;
        lblLoading.IsVisible = false;
    }
    private int LoadDataIntoTable()
    {
        string performanceValue = null;

        tableView.Root.Clear();

        // Iterate over each row in the data
        foreach (var row in viewModel.Rows)
        {
            string currentName = null;
            string colliValue = null;
            string totalcollies = null;
            string lastOrder = null;

            // Create a flag variable to track if the current row belongs to the target user
            bool isTargetUser = false;

            // Iterate over each column in the row
            foreach (var column in row)
            {
                if (column.Key == "name_of_worker" && column.Value.ToUpper() == CurrentName)
                {
                    currentName = $"Name: {column.Value}";
                    isTargetUser = true; // Set flag to true if the row belongs to the target user
                }
                else if (column.Key == "Collected_Per_Hour" && isTargetUser)
                {
                    // Only assign performance value if the current row belongs to the target user
                    colliValue = $"Performance: {column.Value} colli/hr";
                    performanceValue = column.Value;
                }
                else if (column.Key == "Collected_Total" && isTargetUser)
                {
                    totalcollies = $"You've picked {column.Value} colli ";
                }
                else if (column.Key == "Last_Task" && isTargetUser)
                {
                    lastOrder = column.Value;
                }
            }

            // If all necessary data is found, create a new TableSection and add it to the table
            if (isTargetUser && currentName != null && colliValue != null && totalcollies != null && lastOrder != null)
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

        if (performanceValue != null)
        {
            performanceValue = performanceValue.Trim();
            bool success = Int32.TryParse(performanceValue, out int performance);
            if (!success)
            {
                DisplayAlert("Problem", $"Problem with performance value: {performanceValue}", "OK");
            }
            else
            {
                return performance; // Return performance if parse is successful
            }
        }

        // Return 0 or a default value if performanceValue is null or could not be parsed
        return 0;
    }

    private bool IsUserFound()
    {
        foreach (var row in viewModel.Rows)
        {
            foreach (var column in row)
            {
                if (column.Key == "name_of_worker" && column.Value.ToUpper() == CurrentName)
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