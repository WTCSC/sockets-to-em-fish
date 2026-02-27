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

            if (textBox1.Text == "" || textBox1.Text == null) 
            {
                MessageBox.Show("An address is required.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else if (textBox2.Text == "" || textBox2.Text == null)
            {
                MessageBox.Show("A port is required.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else if (textBox3.Text == "" || textBox3.Text == null)
            {
                MessageBox.Show("A username is required.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else 
            { 
                //close the config box and attempt to connect to the server
                this.Hide();
                int.TryParse(textBox2.Text, out serverPort);
                SocketManager.ConnectToServer(textBox1.Text, serverPort, textBox3.Text);
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
