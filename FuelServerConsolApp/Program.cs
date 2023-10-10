using FuelResponseLibrary;
using FuelServerConsolApp;
using Microsoft.EntityFrameworkCore;
using RefuelingLibrary;
using System.IO;
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
        IPEndPoint iPEndPoint1 = new IPEndPoint(serverAddr, 4444);
        TcpListener listener1 = new TcpListener(iPEndPoint);
        TcpListener listener2 = new TcpListener(iPEndPoint1);
        listener1.Start();
        listener2.Start();
        try
        {
            
            Console.WriteLine("Server started");
            while (true)
            {
                _ = Task.Run(async () => await ProcessLogInAsync(listener1));
                _ = Task.Run(async () => await ProsessReFuelAsync(listener2));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            listener1?.Stop();
            listener2?.Stop();
        }
    }
    async static Task ProsessReFuelAsync(TcpListener listener)
    {
        // получаем подключение в виде TcpClient
        using var tcpClient = await listener.AcceptTcpClientAsync();
        // получаем объект NetworkStream для взаимодействия с клиентом
        var streamFuel = tcpClient.GetStream();
        // буфер для считывания размера данных
        byte[] sizeBuff = new byte[4];
        // сначала считываем размер данных
        await streamFuel.ReadAsync(sizeBuff, 0, sizeBuff.Length);
        // узнаем размер и создаем соответствующий буфер
        int sizef = BitConverter.ToInt32(sizeBuff, 0);
        // создаем соответствующий буфер
        byte[] dataf = new byte[sizef];
        // считываем собственно данные
        int bytesf = await streamFuel.ReadAsync(dataf);
        var messagef = Encoding.UTF8.GetString(dataf, 0, bytesf);
        var refuel = JsonSerializer.Deserialize<Refuel>(messagef);
        Console.WriteLine($"Recive : {sizef} byte");

        using (FuelDbContext db = new FuelDbContext())
        {
            db.Refuels.Add(new Refuel() { AddFuel = refuel?.AddFuel, OddFuel = refuel?.OddFuel, Date = refuel!.Date, 
                LocationId = refuel.Location!.LocationId, TrackId = refuel.Track?.TrackId, Odometr = refuel.Odometr });
            //db.Entry(refuel).State = EntityState.Added;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }

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
            var person = db.Persons.First(x => x.Name == user!.Name && x.Password == user.Password);
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
                //var refuelList = db.Refuels.Include(u=>u.Track).Include(u=> u.Location).Where(u=> u.LocationId == person.Location!.LocationId).ToList();
                var refuelResponse = db.Refuels.Select(p => new RefuelShow
                {
                    Date = p.Date.ToString("dd.MM.yyyy"),
                    AddFuel = p.AddFuel.ToString(),
                    OddFuel = p.OddFuel.ToString(),
                    Odometr = p.Odometr.ToString(),
                    LocationName = p.Location.LocationName,
                    RegNum = p.Track!.RegNum
                });
                foreach (var item in refuelResponse)
                {
                    startdata.refuelShows.Add(item);
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