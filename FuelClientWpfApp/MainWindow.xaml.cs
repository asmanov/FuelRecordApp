using System;
using System.Windows;
using UserLibrary;
using System.Text.Json;
using System.Net.Sockets;
using System.Windows.Documents;
using RefuelingLibrary;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FuelClientWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Refuel> refuils = new List<Refuel>();
        TcpClient tcpClient = new TcpClient();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            if (String.IsNullOrEmpty(personName.Text))
            {
                MessageBox.Show("Вы не ввели имя!");
                return;
            }
            else if (String.IsNullOrEmpty(personPass.Password))
            {
                MessageBox.Show("Вы не ввели пароль!");
                return;
            }
            else
            {
                person.Name = personName.Text;
                person.Password = personPass.Password;
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
                    var response = Encoding.UTF8.GetString(dataResponse);
                    if (response == "ERROR")
                    {
                        MessageBox.Show("Неправильное Имя или Пароль");
                    }
                    else
                    {

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                

            }
        }
    }
}
