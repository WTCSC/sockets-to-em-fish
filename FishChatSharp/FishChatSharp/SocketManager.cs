using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;

namespace FishChatSharp
{
    class SocketManager
    {
        public static Socket clientSocket = new Socket(System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
        
        public static void ConnectToServer(string serverIp, int serverPort, string userName) {
            try
            {
                
                clientSocket.Connect(serverIp, serverPort);
                SendData("..$CLNTMSG USERNAME: " + userName); 
            }
            catch (SocketException) 
            {
                MessageBox.Show("The specified port or address is invalid.", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static string ReceiveData() 
        {
            byte[] buffer = new byte[1024];
            try
            {
                int incomingData = clientSocket.Receive(buffer);
                char[] dataChars = new char[incomingData];

                Decoder decoder = Encoding.ASCII.GetDecoder();
                int charLen = decoder.GetChars(buffer, 0, incomingData, dataChars, 0);
                string receivedData = new string(dataChars);
                return receivedData;
            }
            catch (SocketException) 
            {
                MessageBox.Show("The connection was forcibly closed by the server.", "Connection Closed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Application.Exit();
                return null;
            }
            
        }

        public static void SendData(string textToSend) 
        {
            byte[] sentData = Encoding.ASCII.GetBytes(textToSend);
            clientSocket.Send(sentData);
        }

    }
}
