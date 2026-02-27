using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishChatSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string messageContents;
        private void button1_Click(object sender, EventArgs e)
        {
            //grab the contents of textBox1, send a message using that, then clear the box.
            messageContents = textBox1.Text;
            SocketManager.SendData(messageContents);
            textBox1.Clear();
        }

        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form configForm = new Form2();
            configForm.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void SetTimer() 
        {
            //This timer makes the SocketManager check for new messages and updates the message log (listBox1) every second.
            System.Timers.Timer chatLogTimer = new System.Timers.Timer(1000);
            chatLogTimer.Elapsed += chatLogTimer_Elapsed;
            chatLogTimer.AutoReset = true;
            chatLogTimer.Enabled = true;
        }


        private void chatLogTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (SocketManager.clientSocket.Connected)
            {
                toolStripStatusLabel1.Text = "Connected";
                textBox1.Invoke((MethodInvoker)delegate
                {
                    textBox1.Enabled = true;
                });
                button1.Invoke((MethodInvoker)delegate
                {
                    button1.Enabled = true;
                });
                string recv = SocketManager.ReceiveData();
                listBox1.BeginInvoke((MethodInvoker)delegate
                {
                    if (recv != null)
                    {
                        //add message to message log (listBox1)
                        listBox1.Items.Add(recv);
                        //keep box scrolled to the bottom
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                    }
                });
            }
            else 
            {
                statusStrip1.Invoke((MethodInvoker)delegate
                {
                    toolStripStatusLabel1.Text = "Not Connected";
                    
                    
                });
                textBox1.Invoke((MethodInvoker)delegate 
                { 
                    textBox1.Enabled = false;
                });
                button1.Invoke((MethodInvoker)delegate 
                { 
                    button1.Enabled = false;
                });
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetTimer();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
