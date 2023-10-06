using FuelResponseLibrary;
using GalaSoft.MvvmLight.Command;
using RefuelingLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using UserLibrary;

namespace FuelClientWpfApp.ViewModel
{
    internal class FuelAppVM : INotifyPropertyChanged
    {
        private RelayCommand logInCommand;
        private RelayCommand addRefuelCommand;
        private string personName;
        private string personPass;
        private TcpClient tcpClient;
        private ObservableCollection<Location>? locations;
        private ObservableCollection<Track>? tracks;
        private ObservableCollection<RefuelShow>? refuelShows;
        private Location selectedLocation;
        private DateTime selectedDate;
        private Track selectedtrack;
        private string enterOddFuel;
        private string enterAddFuel;
        private string enterOdometr;
        private string selectedDatestring;

        public ObservableCollection<Location>? Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                OnPropertyChanged("Locations");
            }
        }
        public ObservableCollection<Track>? Tracks
        {
            get { return tracks; }
            set
            {
                tracks = value;
                OnPropertyChanged("Tracks");
            }
        }
        public ObservableCollection<RefuelShow>? DataShows
        {
            get { return refuelShows; }
            set
            {
                refuelShows = value;
                OnPropertyChanged("DataShows");
            }
        }

        public RelayCommand LogInCommand
        {
            get
            {
                logInCommand ??= new RelayCommand(ButtonLogIn);
                return logInCommand;
            }
        }

        public RelayCommand AddRefuelCommand
        {
            get
            {
                if (addRefuelCommand == null) addRefuelCommand = new RelayCommand(NewReFuel);
                return addRefuelCommand;
            }
        }

        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                OnPropertyChanged("PersonName");
            }
        }

        public string PersonPass
        {
            get { return personPass; }
            set
            {
                personPass = value;
                OnPropertyChanged("PersonPass");
            }
        }

        public Location SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public string SelectedDatestring
        {
            get { return selectedDatestring; }
            set
            {
                selectedDatestring = value;
                OnPropertyChanged("SelectedDatestring");
            }
        }

        public Track SelectedTrack
        {
            get { return selectedtrack; }
            set
            {
                selectedtrack = value;
                OnPropertyChanged("SelectedTrack");
            }
        }

        public string EnterOddFuel
        {
            get { return enterOddFuel; }
            set
            {
                enterOddFuel = value;
                OnPropertyChanged("EnterOddFuel");
            }
        }

        public string EnterAddFuel
        {
            get { return enterAddFuel; }
            set
            {
                enterAddFuel = value;
                OnPropertyChanged("EnterAddFuel");
            }
        }

        public string EnterOdometr
        {
            get { return enterOdometr; }
            set
            {
                enterOdometr = value;
                OnPropertyChanged("EnterOdometr");
            }
        }

        public FuelAppVM()
        {
            
        }

        private void NewReFuel()
        {
            MessageBox.Show("Click button");
            RefuelShow refuelShow = new RefuelShow();
            SelectedDatestring = SelectedDate.ToString("d");
            refuelShow.Date = SelectedDatestring;
            refuelShow.AddFuel = EnterAddFuel;
            refuelShow.OddFuel = EnterOddFuel;
            refuelShow.Odometr = EnterOdometr;
            refuelShow.LocationName = SelectedLocation.LocationName;
            refuelShow.RegNum = SelectedTrack.RegNum;
            DataShows?.Add(refuelShow);
        }

        private async void ButtonLogIn()
        {
            Person person = new Person();
            tcpClient = new TcpClient();
            if (String.IsNullOrEmpty(personName))
            {
                MessageBox.Show("Вы не ввели имя!");
                return;
            }
            else if (String.IsNullOrEmpty(personPass))
            {
                MessageBox.Show("Вы не ввели пароль!");
                return;
            }
            else
            {
                person.Name = personName;
                person.Password = personPass;
                var jsonPerson = JsonSerializer.Serialize<Person>(person);
                try
                {
                    await tcpClient.ConnectAsync("127.0.0.1", 8888);
                    var stream = tcpClient.GetStream();
                    //пользователь в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(jsonPerson);
                    //определяем размер данных
                    byte[] size = BitConverter.GetBytes(data.Length);
                    //отправляем размер данных
                    await stream.WriteAsync(size, 0, size.Length);
                    //отправляем данные
                    await stream.WriteAsync(data, 0, data.Length);
                    //
                    //
                    //
                    byte[] sizeBuffer = new byte[4];
                    //
                    await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                    //
                    int sizeResponse = BitConverter.ToInt32(sizeBuffer, 0);
                    byte[] dataResponse = new byte[sizeResponse];
                    int bytes = await stream.ReadAsync(dataResponse);

                    tcpClient.Close();
                    var jsonresponse = Encoding.UTF8.GetString(dataResponse);

                    FirstServerResponse response = JsonSerializer.Deserialize<FirstServerResponse>(jsonresponse);
                    MessageBox.Show(response?.ResultUser);
                    if (response?.ResultUser != "Ok")
                    {
                        MessageBox.Show(response?.ResultUser);
                        //tcpClient.Close();
                    }
                    else
                    {
                        Locations = new ObservableCollection<Location>();
                        Locations.Clear();
                        Tracks = new ObservableCollection<Track>();
                        Tracks.Clear();
                        DataShows = new ObservableCollection<RefuelShow>();
                        
                        foreach (var item in response.locations)
                        {

                            Locations.Add(item);
                        }
                        foreach (var item in response.tracks)
                        {

                            Tracks.Add(item);
                        }
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    
                }


            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
