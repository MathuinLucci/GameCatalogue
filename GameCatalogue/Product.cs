using System.Drawing;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Configuration;
namespace GameCatalogue
{
    
    
    /// <summary>
    /// Represents a single game from the API
    /// </summary>
    public class Products
    {
        /// <summary>
        /// The unique identifier for the game, used to fetch more details about the game from the API. Required for items to load correctly.
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// The image of the game from the API. Not always the box art, sometimes a screenshot or missing altogether. Games load correctly without an image.
        /// </summary>
        public Image? ArtImage { get; set; } = null!;
        /// <summary>
        /// Game Title, used to display the name of the game in the UI. Not always present, but games load correctly without a title.
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// The platform on which the game is available. Not always present, but games load correctly without a platform.
        /// </summary>
        public string Platform { get; set; } = null!;
        /// <summary>
        /// The rating of a game returned from the API. Not always presesnt. Games load correctly without a rating.
        /// </summary>
        public string Rating { get; set; } = null!;
        /// <summary>
        /// Release date of a game. May not always be present. Games load correctly without a release date.
        /// </summary>
        public string ReleaseDate { get; set; } = null!;
    }
}