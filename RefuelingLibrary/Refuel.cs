using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RefuelingLibrary
{
    public class Refuel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public int? AddFuel { get; set; }
        public int? OddFuel { get; set; }
        public int? Odometr { get; set; }

        public Track? Track { get; set; }
        public Location? Location { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Track
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; } = string.Empty;
        public string RegNum { get; set; } = string.Empty;
        public string? VIN { get; set; }
        public int? VTunk { get; set; }
    }

    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
    }
}