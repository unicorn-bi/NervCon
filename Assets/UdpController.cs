using UnityEngine;
using System.Net;
using System.Text;
using UDP;

public class UdpController : MonoBehaviour
{
    private UDPSender udpSender;

    private string remoteIP = "127.0.0.1"; 
    private string remotePort = "5000";        

    void Start()
    {
        udpSender = new UDPSender(); // Initialize UDP client
        udpSender.Open(remoteIP, remotePort);
        //SendData(1);

    }

    // Method to send any serializable object as JSON over UDP
    public void SendData(string selectedCommang)
    {
        udpSender.Send(selectedCommang);
    }

    void OnDestroy()
    {
        if (udpSender != null)
        {
            udpSender.Close(); // Close the UDP client when the script is destroyed
        }
    }
}
