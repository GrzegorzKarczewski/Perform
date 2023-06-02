namespace Perform.ViewModels
{
    public class CsvDataViewModel
    {
        public List<Dictionary<string, string>> Rows { get; private set; }

        public CsvDataViewModel()
        {

        }

        public async Task LoadDataAsync()
        {
            string csvData = await GetCsvDataAsync();
            Rows = ParseCsv(csvData);
        }

        private static async Task<string> GetCsvDataAsync()
        {
            var client = new HttpClient();
            string csvData = await client.GetStringAsync("https://orderpickloremipsum.azurewebsites.net/StaticFiles/output.csv");
            return csvData;
        }

        private List<Dictionary<string, string>> ParseCsv(string csvData)
        {
            var lines = csvData.Split('\n');
            var headers = lines[0].Split(',');

            var rows = new List<Dictionary<string, string>>();
            for (var i = 1; i < lines.Length; i++)
            {
                var row = new Dictionary<string, string>();
                var columns = lines[i].Split(',');

                for (int j = 0; j < headers.Length; j++)
                {
                    if (j < columns.Length)
                    {
                        row[headers[j]] = columns[j];
                    }
                    else
                    {
                        row[headers[j]] = null; // or some default value
                    }
                }

                rows.Add(row);
            }

            return rows;
        }
    }
}
