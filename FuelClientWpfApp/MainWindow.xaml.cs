using FuelClientWpfApp.ViewModel;
using System.Windows;

namespace FuelClientWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new FuelAppVM();

        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Person person = new Person();
        //    if (String.IsNullOrEmpty(personName.Text))
        //    {
        //        MessageBox.Show("Вы не ввели имя!");
        //        return;
        //    }
        //    else if (String.IsNullOrEmpty(personPass.Password))
        //    {
        //        MessageBox.Show("Вы не ввели пароль!");
        //        return;
        //    }
        //    else
        //    {
        //        person.Name = personName.Text;
        //        person.Password = personPass.Password;
        //        var jsonPerson = JsonSerializer.Serialize<Person>(person);
        //        try
        //        {
        //            await tcpClient.ConnectAsync("127.0.0.1", 8888);
        //            var stream = tcpClient.GetStream();
        //            //пользователь в массив байтов
        //            byte[] data = Encoding.UTF8.GetBytes(jsonPerson);
        //            //определяем размер данных
        //            byte[] size = BitConverter.GetBytes(data.Length);
        //            //отправляем размер данных
        //            await stream.WriteAsync(size, 0, size.Length);
        //            //отправляем данные
        //            await stream.WriteAsync(data, 0, data.Length);
        //            //
        //            //
        //            //
        //            byte[] sizeBuffer = new byte[4];
        //            //
        //            await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
        //            //
        //            int sizeResponse = BitConverter.ToInt32(sizeBuffer, 0);
        //            byte[] dataResponse = new byte[sizeResponse];
        //            int bytes = await stream.ReadAsync(dataResponse);
        //            var jsonresponse = Encoding.UTF8.GetString(dataResponse);
        //            var response = JsonSerializer.Deserialize<FirstServerResponse>(jsonresponse);
        //            if (response.ResultUser != "Ok")
        //            {
        //                MessageBox.Show(response.ResultUser);
        //            }
        //            else
        //            {

        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            MessageBox.Show(ex.ToString());
        //        }


        //    }
        //}
    }
}
