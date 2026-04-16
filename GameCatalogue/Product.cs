using System.Drawing;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Configuration;
namespace GameCatalogue
{
    public class Products
    {
        public int GameId { get; set; }
        public Image ArtImage { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public string Rating { get; set; }
        public string ReleaseDate { get; set; }
    }
}