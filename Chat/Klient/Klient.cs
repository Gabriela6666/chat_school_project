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
                TcpClient client = new TcpClient(host, port);
                Client.Items.Add("Nawiazano połączenie z " + host + " na porcie: " + port);
                client.Close();
            }
            catch (Exception ex)
            {
                Client.Items.Add("Błąd: Nie udało się nawiązać połączenia!");
                MessageBox.Show(ex.ToString());

            }
        }
    }
}
