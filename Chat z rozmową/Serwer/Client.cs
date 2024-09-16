using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Serwer
{
    class Client
    {
        public TcpClient client;
        public Thread message;
        public String nazwa;
        public String connectionTime;
        public StreamReader reading;
        public StreamWriter writing;

        public Client(ref TcpClient newConnection, ref StreamReader newStreamRead, ref String newNick, String whenConnect)
        {
            client = newConnection;
            nazwa = newNick;
            connectionTime = whenConnect;
            reading = newStreamRead;
            writing = new StreamWriter(client.GetStream());

            //wyswietlanie rozmowy
            message = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Serv.Broadcast(reading.ReadLine(), ref nazwa);
                    }
                    catch (Exception ex)
                    {
                        Serv.Delete(this);
                    }
                }
            });
            message.Start();
        }
    }
}
