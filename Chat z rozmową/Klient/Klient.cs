using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Klient
{
    public partial class Klient : Form
    {
        private TcpClient client = null;
        private StreamWriter writing = null;
        private StreamReader reading = null;
        private Thread message;

        //private bool activeCall = false;

        public Klient()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string host = textBox2.Text;
            int port = System.Convert.ToInt16(numericUpDown1.Value);
            try
            {
                client = new TcpClient(host, port);
                client.Connect(host, port);
                reading = new StreamReader(client.GetStream());
                writing = new StreamWriter(client.GetStream());
                Client.Items.Add("Nawiązano połączenie z " + host + " na porcie: " + port);
                //odczytywanie
                message = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            Client.Items.Add(reading.ReadLine());
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                });
                message.Start();
                //client.Close();
            }
            catch (Exception ex)
            {
                Client.Items.Add("Błąd: Nie udało się nawiązać połączenia!");
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //wysylanie
            string wiadomosc = textBox1.Text;
            writing.WriteLine(wiadomosc);
            Client.Items.Add(wiadomosc); //niepotrzebne
        }
    }
}
