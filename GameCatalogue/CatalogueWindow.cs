using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.Json;

namespace GameCatalogue
{
    public partial class CatalogueWindow : Form
    {
        private Dictionary<string, int> platforms = new();
        public CatalogueWindow()
        {
            InitializeComponent();
            AcceptButton = this.btnSearch;
            CancelButton = this.btnClose;
            hdrBoxart.Width = 120;
            hdrBoxart.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            hdrPlatform.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            hdrPlatform.FillWeight = 40;
            hdrRating.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            hdrRating.FillWeight = 20;
            hdrReleaseDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            hdrReleaseDate.FillWeight = 20;
            hdrTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            hdrTitle.FillWeight = 40;
            catalogueTable.AutoGenerateColumns = false;
            catalogueTable.RowTemplate.Height = 120;
            hdrBoxart.ImageLayout = DataGridViewImageCellLayout.Zoom;
            catalogueTable.ColumnHeaderMouseClick += catalogueTable_ColumnHeaderMouseClick;
            cboResults.Items.AddRange(new object[] { "5", "10", "20", "40" });
            cboResults.SelectedIndex = 2;
            platforms = new Dictionary<string, int>
            {
                {"All" , -1 },
                {"Playstation" , 27 },
                {"PlayStation 2" , 15 },
                {"PlayStation 3" , 16 },
                {"PlayStation 4", 18},
                {"PlayStation 5", 187},
                {"PSP" , 17 },
                {"PS Vita" , 19 },
                {"Xbox" , 14 },
                {"Xbox One", 1},
                {"Xbox Series S/X", 186},
                {"SNES", 79},
                {"NES", 49},
                {"Nintendo 64", 83},
                {"Nintendo GameCube", 105},
                {"Wii", 11},
                {"Wii U", 10},
                {"Nintendo Switch", 7},
                {"Game Boy", 26},
                {"Game Boy Color", 43},
                {"Game Boy Advance", 24},
                {"Nintendo 3DS", 8},
                {"Nintendo DS", 9},
                {"Nintendo DSi", 13},
                {"Atari 2600", 23},
                {"Atari 5200", 31},
                {"Atari 7800", 28},
                {"Atari Lynx", 46},
                {"Atari 8-bit", 25},
                {"Atari ST", 34},
                {"Atari XEGS", 50},
                {"Atari Flashback", 22},
                {"SEGA Genesis", 167},
                {"SEGA Saturn", 107},
                {"SEGA CD", 119},
                {"SEGA 32X", 117},
                {"SEGA Master System", 74},
                {"SEGA Dreamcast", 106},
                {"SEGA Game Gear", 77},
                {"Neo Geo", 12},
                {"Apple II", 41},
                {"Commodore / Amiga", 166},
                {"PC", 4},
                {"Linux", 6},
                {"Classic Macintosh", 55},
                {"macOS", 5},
                {"iOS", 3},
                {"Android", 21}
            };

            var platformList = new List<KeyValuePair<string, int>>();

            cboPlatform.DataSource = platformList;
            cboPlatform.DisplayMember = "Key";
            cboPlatform.ValueMember = "Value";


            // Bind dictionary to the ComboBox
            // Dictionary<TKey,TValue> translates into KeyValuePair<TKey,TValue>.
            // Use a list of KeyValuePair and bind Key/Value property names.
            cboPlatform.DataSource = platforms.ToList();
            cboPlatform.DisplayMember = "Key";
            cboPlatform.ValueMember = "Value";
        }

        /// <summary>
        /// Loads the JSON data from the provided URL using an HttpClient. It sends a GET request to the URL and retrieves the response as a string. The method is asynchronous and returns the JSON data as a string when the request is complete.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> GetDataFromAPI(string url)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return json;
        }

        /// <summary>
        /// Loads all results from the API based on the provided URL and page size. It uses a loop to fetch data page by page until there are no more results to retrieve. The method constructs the URL with pagination parameters, retrieves the JSON data from the API, deserializes it into a RawgGameResponse object, and adds the results to a list of RawgGame objects. If there are no results or if the number of results is less than the specified page size, it breaks the loop and returns the complete list of games.
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<RawgGame>> LoadAllResultsAsync(string urlBase, int pageSize)
        {
            var allGames = new List<RawgGame>();
            int page = 1;

            while (true)
            {
                string url = $"{urlBase}&page={page}&page_size={pageSize}";
                string json = await GetDataFromAPI(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                RawgGameResponse? data = JsonSerializer.Deserialize<RawgGameResponse>(json, options);

                if (data == null || data.results == null || data.results.Count == 0)
                    break;

                allGames.AddRange(data.results);

                if (data.results.Count < pageSize)
                    break;

                page++;
            }

            return allGames;
        }

        /// <summary>
        /// builds the basic URL using the necessary data to bring back results
        /// </summary>
        /// <returns></returns>
        public string URLBuilder()
        {
            string apiKey = ConfigurationManager.AppSettings["RawgApiKey"];
            string urlBase = $"https://api.rawg.io/api/games?key={apiKey}{Filter()}{SearchText()}";
            return urlBase;
        }


        /// <summary>
        /// validates the text in the search box
        /// </summary>
        /// <returns></returns>
        private bool EmptyText()
        {
            bool isEmpty = false;
            if (String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                isEmpty = true;
            }
            return isEmpty;
        }

        /// <summary>
        /// Allows filtering of reults by platform ID. If "All" (-1) is selected, it is blank and returns all platforms
        /// </summary>
        /// <returns></returns>
        private string Filter()
        {
            int id = (int)cboPlatform.SelectedValue;
            if (id == -1)
                return "";

            return $"&platforms={id}";
        }


        /// <summary>
        /// Takes the text from the search box and adds it to the API call if it's not empty
        /// </summary>
        /// <returns></returns>
        private string SearchText()
        {
            string search = "";

            if (!EmptyText())
            {
                string formatURL = Uri.EscapeDataString(txtSearch.Text);
                search = $"&search={formatURL}";
            }
            return search;
        }

        /// <summary>
        /// builds the image from the URL. If the URL is empty or invalid, it returns null
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<Image?> ImageBuilder(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            using HttpClient client = new HttpClient();

            try
            {
                byte[] data = await client.GetByteArrayAsync(url);
                using var ms = new MemoryStream(data);
                return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Loads a single page of results based on the provided URL, page number, and page size. It constructs the URL with pagination parameters, retrieves the JSON data from the API, deserializes it into a RawgGameResponse object, and then maps the results to a list of Products objects. Each product includes the game ID, title, platform(s), rating, release date, and art image (which is loaded asynchronously). If there are no results, it returns an empty list.
        /// </summary>
        /// <param name="urlBase"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<List<Products>> LoadSinglePageAsync(string urlBase, int page, int pageSize)
        {
            string url = $"{urlBase}&page={page}&page_size={pageSize}";
            string json = await GetDataFromAPI(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            RawgGameResponse? response = JsonSerializer.Deserialize<RawgGameResponse>(json, options);

            if (response?.results == null)
                return new List<Products>();

            var products = await Task.WhenAll(response.results.Select(async g => new Products
            {
                GameId = g.id,
                ArtImage = await ImageBuilder(g.background_image),
                Title = g.name,
                Platform = g.platforms == null
                    ? "Platform Not Listed"
                    : string.Join(", ", g.platforms.Select(x => x.platform.name)),
                Rating = g.rating.ToString(),
                ReleaseDate = g.released
            }));

            return products.ToList();
        }

        /// <summary>
        /// Takes the search text, selected platform, and results per page to build the URL and load the first page of results. Also resets the current page to 1 and updates the page label. Disables the buttons and shows a wait cursor while loading to prevent multiple clicks and confusion.
        /// </summary>
        private int currentPage = 1;
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            currentPage = 1;
            lblPage.Text = $"Page: {currentPage}";

            int pageSize = int.Parse(cboResults.Text);

            var products = await LoadSinglePageAsync(URLBuilder(), currentPage, pageSize);
            catalogueTable.DataSource = products;

            UseWaitCursor = false;
            ApplyLastSort();
        }

        /// <summary>
        /// When you click the ""Next" button, goes forward one page. If there are no results on the next page, it goes back to the current page and doesn't change anything. Disables the buttons and shows a wait cursor while loading to prevent multiple clicks and confusion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnNext_Click(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            UseWaitCursor = true;

            currentPage++;
            lblPage.Text = $"Page: {currentPage}";

            int pageSize = int.Parse(cboResults.Text);

            var products = await LoadSinglePageAsync(URLBuilder(), currentPage, pageSize);

            if (products.Count == 0)
            {
                currentPage--;
                lblPage.Text = $"Page: {currentPage}";
            }
            else
            {
                catalogueTable.DataSource = products;
            }

            btnNext.Enabled = true;
            btnPrev.Enabled = true;
            UseWaitCursor = false;
            ApplyLastSort();
        }

        /// <summary>
        /// When you click the "Prev" button, goes back one page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage == 1)
                return;

            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            UseWaitCursor = true;

            currentPage--;
            lblPage.Text = $"Page: {currentPage}";

            int pageSize = int.Parse(cboResults.Text);
            var products = await LoadSinglePageAsync(URLBuilder(), currentPage, pageSize);
            catalogueTable.DataSource = products;

            btnNext.Enabled = true;
            btnPrev.Enabled = true;
            UseWaitCursor = false;
            ApplyLastSort();
        }

        /// <summary>
        /// Allows you to click a header to sort by that column, and click it again to reverse the sort. Remembers the last sort method and applies it when you change pages or search again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void catalogueTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = catalogueTable.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(columnName))
                return;

            var data = (List<Products>)catalogueTable.DataSource;

            bool ascending = true;

            if (catalogueTable.Tag is Tuple<string, bool> lastSort &&
                lastSort.Item1 == columnName)
            {
                ascending = !lastSort.Item2;
            }

            catalogueTable.Tag = Tuple.Create(columnName, ascending);

            if (ascending)
                catalogueTable.DataSource = data.OrderBy(x => x.GetType().GetProperty(columnName)?.GetValue(x)).ToList();
            else
                catalogueTable.DataSource = data.OrderByDescending(x => x.GetType().GetProperty(columnName)?.GetValue(x)).ToList();
        }

        /// <summary>
        /// Remembers you want to sort by a certain method and applies it automatically when you change pages or search again
        /// </summary>
        private void ApplyLastSort()
        {
            if (catalogueTable.Tag is not Tuple<string, bool> lastSort)
                return;

            string columnName = lastSort.Item1;
            bool ascending = lastSort.Item2;

            var data = (List<Products>)catalogueTable.DataSource;

            if (ascending)
                catalogueTable.DataSource = data.OrderBy(x => x.GetType().GetProperty(columnName)?.GetValue(x)).ToList();
            else
                catalogueTable.DataSource = data.OrderByDescending(x => x.GetType().GetProperty(columnName)?.GetValue(x)).ToList();
        }

        /// <summary>
        /// Coses the program when the CLose button is clicked or the escape key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}