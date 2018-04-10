using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Net.NetworkInformation;
namespace Chat_Client
{
    public partial class Form1 : Form
    {
        Socket client;
        byte[] data;
        IPEndPoint ipServer;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            textBox2.Text = "";
            listBox1.Items.Add(text);
            client.Send(data);
            data = new byte[1024];
            client.Receive(data);
            text = Encoding.ASCII.GetString(data);
            listBox1.Items.Add(text);
            IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
            textBox1.Text = ip.Address.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
        }
        private static void Connected(IAsyncResult iar)
        {
            Socket sock = (Socket)iar.AsyncState;
            sock.EndConnect(iar);
        }
    }
}
