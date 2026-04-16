using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalogue
{
    public class RawgGameResponse
    {
        public List<RawgGame> results { get; set; }
    }

    public class RawgGame
    {
        public int id { get; set; }
        public string name { get; set; }
        public string released { get; set; }
        public double rating { get; set; }
        public string background_image { get; set; }
        public List<RawgPlatformWrapper> platforms { get; set; }
    }

    public class RawgPlatformWrapper
    {
        public RawgPlatform platform { get; set; }
    }

    public class RawgPlatform
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}