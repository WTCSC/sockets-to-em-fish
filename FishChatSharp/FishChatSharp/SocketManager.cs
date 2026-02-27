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
        //build the socket using the Stream type and the IP protocol
        public static Socket clientSocket = new Socket(System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
        public static bool errorShown;
        public static string currentUserName;

        public static void ConnectToServer(string serverIp, int serverPort, string userName) {
            try
            {
                clientSocket.Connect(serverIp, serverPort);
                //send the username over to the server
                SendData("..$CLNTMSG USERNAME: " + userName); 
                //hold onto the username for future use
                currentUserName = userName;
            }
            catch (SocketException) 
            {
                MessageBox.Show("The specified port or address is invalid.", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            // make sure the error message for if the server dies can show
            errorShown = false;
        }

        

        public static string ReceiveData() 
        {
            //sockets in C# require a buffer, so we're defining it here
            byte[] buffer = new byte[1024];
            try
            {
                //recieve the data and put it in the buffer
                int incomingData = clientSocket.Receive(buffer);
                //we need somewhere to put the characters when decoded
                char[] dataChars = new char[incomingData];

                Decoder decoder = Encoding.ASCII.GetDecoder();
                //decode the buffer into chars and put them into that char list
                int charLen = decoder.GetChars(buffer, 0, incomingData, dataChars, 0);
                //build a string from the char list
                string receivedData = new string(dataChars);
                return receivedData;
            }
            catch (SocketException) 
            {
                //preventing the error from showing 13 quadrillion times
                if (!errorShown) 
                {
                    //immediately set this value so that the if statement actually does something
                    errorShown = true;
                    MessageBox.Show("The connection was forcibly closed by the server.", "Connection Closed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Application.Exit();
                }
                return null;
            }
            
        }

        public static string SendData(string textToSend) 
        {
            //encode the data to send in bytes for sockets
            byte[] sentData = Encoding.ASCII.GetBytes(textToSend);
            clientSocket.Send(sentData);
            //give the username and sent text back to the caller
            return currentUserName + ':' + textToSend;
        }

    }
}
