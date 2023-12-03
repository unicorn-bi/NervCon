using UnityEngine;
using System.Net;
using System.Text;
using UDP;

public class UdpController : MonoBehaviour
{
    private UDPSender udpSender;

    private string remoteIP = "127.0.0.1"; 
    private string remotePort = "5000";
    public bool isOpen = false;

    void Start()
    {
        udpSender = new UDPSender(); // Initialize UDP client
        //OpenChannel();
    }

    public void OpenChannel()
    {
        udpSender.Open(remoteIP, remotePort);
        isOpen = true;
    }

    // Method to send any serializable object as JSON over UDP
    public void SendData(string selectedCommand)
    {
        if(isOpen)
        {
            udpSender.Send(selectedCommand);
        }
    }

    void OnDestroy()
    {
        if (udpSender != null)
        {
            udpSender.Close(); // Close the UDP client when the script is destroyed
        }
    }
}
