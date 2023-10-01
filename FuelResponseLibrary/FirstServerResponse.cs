using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefuelingLibrary;

namespace FuelResponseLibrary
{
    public class FirstServerResponse
    {
        public string ResultUser { get; set; }
        public List<Track> tracks { get; set; }
        public List<Location> locations { get; set; }

        public FirstServerResponse()
        {
            tracks = new List<Track>();
            locations = new List<Location>();
        }
    }
}
