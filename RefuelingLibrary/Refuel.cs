using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RefuelingLibrary
{
    public class Refuel : INotifyPropertyChanged
    {
        private int id;
        private string date;
        private int? addFuel;
        private int? odometr;

        public int Id 
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Date 
        { 
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            } 
        }
        public int? AddFuel 
        { 
            get { return addFuel; } 
            set
            {
                addFuel = value;
                OnPropertyChanged("AddFuel");
            }
        }
        public int? oddFuel 
        { 
            get { return oddFuel; }
            set { oddFuel = value; OnPropertyChanged("OddFuel"); }
        }
        public int? Odometr 
        { 
            get { return odometr; } 
            set
            {
                odometr = value; OnPropertyChanged("Odometr");
            }
        }

        public Track? Track { get; set; }
        public Location? Location { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class RefuelShow : INotifyPropertyChanged
    {
        private int id;
        private string date;
        private int? addFuel;
        private int? odometr;
        private string locationName;
        private string regNum;
        private int? oddFuel;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public int? AddFuel
        {
            get { return addFuel; }
            set
            {
                addFuel = value;
                OnPropertyChanged("AddFuel");
            }
        }
        public int? OddFuel
        {
            get { return oddFuel; }
            set { oddFuel = value; OnPropertyChanged("OddFuel"); }
        }
        public int? Odometr
        {
            get { return odometr; }
            set
            {
                odometr = value; OnPropertyChanged("Odometr");
            }
        }

        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; OnPropertyChanged("LocationName"); }
        }

        public string RegNum
        {
            get { return regNum; }
            set
            {
                regNum = value; OnPropertyChanged("RegNum");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Track : INotifyPropertyChanged
    {
        private int trackId;
        private string trackName;
        private string regNum;
        private string? vin;
        private int? vTunk;

        public int TrackId 
        { 
            get { return trackId;} 
            set
            {
                trackId = value; OnPropertyChanged("TrackId");
            } 
        }
        public string TrackName 
        { 
            get { return trackName;}
            set
            {
                trackName = value; OnPropertyChanged("TrackName");
            }
        } 
        public string RegNum 
        { 
            get { return regNum; }
            set
            {
                regNum = value; OnPropertyChanged("RegNum");
            } 
        } 
        public string? VIN 
        { 
            get { return vin; } 
            set
            {
                vin = value; OnPropertyChanged("VIN");
            }
        }
        public int? VTunk 
        { 
            get { return vTunk;} 
            set { vTunk = value; OnPropertyChanged("VTunk"); } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Location : INotifyPropertyChanged
    {
        private int locationId;
        private string locationName;

        public int LocationId 
        { 
            get { return locationId; } 
            set
            {
                locationId = value; OnPropertyChanged("Location");
            }
        }
        public string LocationName 
        { 
            get { return locationName; } 
            set { locationName = value; OnPropertyChanged("LocationName"); } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}