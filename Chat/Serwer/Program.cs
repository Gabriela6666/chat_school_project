using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serwer
{
    class Serv
    {
        static string adres;
        static int port;
        static TcpListener server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 100));
        static List<string> names = new List<string>();
        static List<Client> users = new List<Client>(); 

        static void Main(string[] args)
        {
            Console.WriteLine("Server Address: ");
            adres = Console.ReadLine();
            Console.WriteLine("Port: ");
            string line = Console.ReadLine();
            try
            {
                port = int.Parse(line);
                server = new TcpListener(new IPEndPoint(IPAddress.Parse(adres), port));
                server.Start(0);
            }
            catch (Exception ex)
            {
                server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 100));
                server.Start(0);
            }

            Thread waitForConnect = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Server working...");
                    TcpClient tcpClient = server.AcceptTcpClient();
                    StreamReader streamReader = new StreamReader(tcpClient.GetStream());
                    String nazwa = streamReader.ReadLine();
                    Console.WriteLine(nazwa);
                    while (names.Contains(nazwa))
                    {
                        nazwa += "-sobowtór";
                    }
                    names.Add(nazwa);
                    users.Add(new Client(ref tcpClient, ref streamReader, ref nazwa, DateTime.Now.ToString("h:mm:ss tt")));
                }
            });
            waitForConnect.Start();
        }

        public static void Broadcast(String message, ref String sender)
        {
            foreach (Client client in users)
            {
                client.writing.WriteLine(" <b>" + sender + "</b>: " + message);
                client.writing.Flush();
            }
        }

        public static void Delete(Client disconnectedClient)
        {
            names.Remove(disconnectedClient.nazwa);
            users.Remove(disconnectedClient);
            disconnectedClient.message.Abort();
        }
    }
}
