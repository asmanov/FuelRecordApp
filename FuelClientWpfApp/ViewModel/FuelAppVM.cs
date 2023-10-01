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
        //private RelayCommand addRefuelCommand;
        private string personName;
        private string personPass;
        private TcpClient tcpClient;
        private ObservableCollection<Location>? locations;
        private ObservableCollection<Track>? tracks;

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


        public RelayCommand LogInCommand
        {
            get
            {
                logInCommand ??= new RelayCommand(ButtonLogIn);
                return logInCommand;
            }
        }

        //public RelayCommand AddRefuelCommand
        //{
        //    get
        //    {
        //        if (addRefuelCommand == null) addRefuelCommand = new RelayCommand();
        //        return addRefuelCommand;
        //    }
        //}

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

        public FuelAppVM()
        {
            
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
