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

        string apiKey = ConfigurationManager.AppSettings["RawgApiKey"];

        private async Task<string> GetDataFromAPI(string url)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return json;
        }

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

        private bool EmptyText()
        {
            bool isEmpty = false;
            if (String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                isEmpty = true;
            }
            return isEmpty;
        }

        private string PageSize()
        {
            int pageSize = Convert.ToInt32(cboResults.Text);
            string pageUrl = $"&page_size={pageSize}";
            return pageUrl;
        }

        private string Filter()
        {
            int id = (int)cboPlatform.SelectedValue;
            if (id == -1)
                return "";

            return $"&platforms={id}";
        }

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

        private Image ImageBuilder(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            using (var client = new WebClient())
            {
                byte[] data = client.DownloadData(url);
                using (var ms = new MemoryStream(data))
                {
                    return Image.FromStream(ms);
                }
            }
        }

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

            return response.results.Select(g => new Products
            {
                GameId = g.id,
                ArtImage = ImageBuilder(g.background_image),
                Title = g.name,
                Platform = g.platforms == null
                    ? "Platform Not Listed"
                    : string.Join(", ", g.platforms.Select(x => x.platform.name)),
                Rating = g.rating.ToString(),
                ReleaseDate = g.released
            }).ToList();
        }


        private int currentPage = 1;
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            currentPage = 1;
            lblPage.Text = $"Page: {currentPage}";

            int pageSize = int.Parse(cboResults.Text);

            string urlBase =
                $"https://api.rawg.io/api/games?key={apiKey}{Filter()}{SearchText()}";

            var products = await LoadSinglePageAsync(urlBase, currentPage, pageSize);
            catalogueTable.DataSource = products;

            UseWaitCursor = false;
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            UseWaitCursor = true;

            currentPage++;
            lblPage.Text = $"Page: {currentPage}";

            int pageSize = int.Parse(cboResults.Text);
            string urlBase = $"https://api.rawg.io/api/games?key={apiKey}{Filter()}{SearchText()}";

            var products = await LoadSinglePageAsync(urlBase, currentPage, pageSize);

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
        }

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
            string urlBase = $"https://api.rawg.io/api/games?key={apiKey}{Filter()}{SearchText()}";

            var products = await LoadSinglePageAsync(urlBase, currentPage, pageSize);
            catalogueTable.DataSource = products;

            btnNext.Enabled = true;
            btnPrev.Enabled = true;
            UseWaitCursor = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}