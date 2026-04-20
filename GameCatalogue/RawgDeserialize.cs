using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalogue
{

    /// <summary>
    /// Returns a list of games from the RAWG API, which is used to populate the game catalogue. It provides a database of video games and deserializes the JSON response from the RAWG API into C# objects.
    /// </summary>
    public class RawgGameResponse
    {
        public List<RawgGame> results { get; set; } = null!;
    }


    /// <summary>
    /// Represents a single game from the RAWG API
    /// </summary>
    public class RawgGame
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Released { get; set; } = null!;
        public double Rating { get; set; }
        public string BackgroundImage { get; set; } = null!;
        public List<RawgPlatformWrapper> Platforms { get; set; } = null!;
    }


    /// <summary>
    /// A helper class that acts as a container class to wrap the platform information from the RAWG API, as the API returns platforms in a nested structure.
    /// </summary>
    public class RawgPlatformWrapper
    {
        public RawgPlatform Platform { get; set; } = null!;
    }


    /// <summary>
    /// 
    /// </summary>
    public class RawgPlatform
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}