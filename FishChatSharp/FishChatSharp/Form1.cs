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
            System.Timers.Timer chatLogTimer = new System.Timers.Timer(1000);
            chatLogTimer.Elapsed += chatLogTimer_Elapsed;
            chatLogTimer.AutoReset = true;
            chatLogTimer.Enabled = true;
        }

        public bool initalMessage;

        private void chatLogTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (SocketManager.clientSocket.Connected) 
            { 
                string prev = listBox1.Items[listBox1.Items.Count - 1].ToString();
                try
                {
                    string recv = SocketManager.ReceiveData().Result;
                    if (prev == recv)
                    {
                        return;
                    }
                    else
                    {
                        listBox1.BeginInvoke((MethodInvoker)delegate
                        {
                            if (recv != null)
                            {
                                if (initalMessage)
                                {
                                    listBox1.Items.Remove("");
                                    listBox1.Items.Add(recv);
                                    initalMessage = false;
                                }
                                else 
                                {
                                    listBox1.Items.Add(recv);
                                }
                                listBox1.TopIndex = listBox1.Items.Count - 1;
                            }
                        });
                    }
                }
                catch (AggregateException)
                {
                    bool boxOpened = false;
                    if (boxOpened = false)
                    {
                        MessageBox.Show("The connection to the server was forcibly terminated.", "Connection Closed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        boxOpened = true;
                    }
                    Application.Exit();
                }
                
                
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("");
            initalMessage = true;
            SetTimer();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
