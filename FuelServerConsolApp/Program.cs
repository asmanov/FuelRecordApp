using FuelResponseLibrary;
using FuelServerConsolApp;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using UserLibrary;

public class Program
{
    public async static Task Main(string[] args)
    {
        IPAddress serverAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint iPEndPoint = new IPEndPoint(serverAddr, 8888);
        TcpListener listener1 = new TcpListener(iPEndPoint);
        listener1.Start();
        try
        {
            
            Console.WriteLine("Server started");
            while (true)
            {
                

                Task.Run(async () => await ProcessLogInAsync(listener1));
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    async static Task ProcessLogInAsync(TcpListener listener1)
    {
        // получаем подключение в виде TcpClient
        using var tcpClient = await listener1.AcceptTcpClientAsync();
        // получаем объект NetworkStream для взаимодействия с клиентом
        var stream = tcpClient.GetStream();

        // буфер для считывания размера данных
        byte[] sizeBuffer = new byte[4];
        // сначала считываем размер данных
        await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
        // узнаем размер и создаем соответствующий буфер
        int size = BitConverter.ToInt32(sizeBuffer, 0);
        // создаем соответствующий буфер
        byte[] data = new byte[size];
        // считываем собственно данные
        int bytes = await stream.ReadAsync(data);
        var message = Encoding.UTF8.GetString(data, 0, bytes);
        var user = JsonSerializer.Deserialize<Person>(message);
        Console.WriteLine($"Size message: {size} byte");
        Console.WriteLine($"User: {user?.Name} user password: {user?.Password}");
        using (FuelDbContext db = new FuelDbContext())
        {
            var person = db.Persons.First(x => x.Name == user.Name && x.Password == user.Password);
            FirstServerResponse startdata = new FirstServerResponse();
            if (person == null)
            {
                startdata.ResultUser = "Неправильное имя или пароль";
            }
            else
            {
                startdata.ResultUser = "Ok";
                foreach (var item in db.Tracks)
                {
                    startdata.tracks.Add(item);
                }
                foreach (var item in db.Locations)
                {
                    startdata.locations.Add(item);
                }
            }
            var jsonResponse = JsonSerializer.Serialize(startdata);
            //пользователь в массив байтов
            byte[] responsedata = Encoding.UTF8.GetBytes(jsonResponse);
            //определяем размер данных
            byte[] responsesize = BitConverter.GetBytes(responsedata.Length);

            try
            {
                //отправляем размер данных
                await stream.WriteAsync(responsesize, 0, responsesize.Length);
                //отправляем данные
                await stream.WriteAsync(responsedata, 0, responsedata.Length);
                Console.WriteLine($"send {responsedata.Length} byte");
            }
            catch (Exception)
            {
                Console.WriteLine("Not send");
            }

        }
    }
}