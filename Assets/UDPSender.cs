using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class UDPSender
    {
        /// <summary>
        /// The udp socket.
        /// </summary>
        private Socket _socket = null;

        /// <summary>
        /// The ip endpoint.
        /// </summary>
        private IPEndPoint _endPoint;

        /// <summary>
        /// The text encoding.
        /// </summary>
        private Encoding _encoding;

        public UDPSender()
        {
            _encoding = System.Text.Encoding.ASCII;
        }

        public void Open(string ip, string port)
        {
            if (_socket == null)
            {
                IPAddress ipAdress = IPAddress.Parse(ip);
                int udpport = Int32.Parse(port);

                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _endPoint = new IPEndPoint(ipAdress, udpport);
                _socket.Connect(_endPoint);
            }
        }

        public void Close()
        {
            if (_socket != null)
            {
                //close socket connection
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                _socket = null;
            }
        }

        public void Send(string command)
        {
            if (_socket != null)
            {
                byte[] bytearray = _encoding.GetBytes(command);
                _socket.SendTo(bytearray, _endPoint);
            }
        }
    }
}
