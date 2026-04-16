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

        public Image? ArtImage { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public string Rating { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
    }
}