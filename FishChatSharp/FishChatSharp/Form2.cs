using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishChatSharp;

namespace FishChatSharp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private int serverPort;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            int.TryParse(textBox2.Text, out serverPort);
            SocketManager.ConnectToServer(textBox1.Text, serverPort);
        }
    }
}
