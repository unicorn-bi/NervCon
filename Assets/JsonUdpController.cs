using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class JsonUdpController : MonoBehaviour
{
    private UdpClient udpClient;
    public string remoteIP = "127.0.0.1"; 
    public int remotePort = 12345;        

    void Awake()
    {
        udpClient = new UdpClient(); // Initialize UDP client
    }

    // Method to send any serializable object as JSON over UDP
    public void SendData<T>(T dataObject)
    {
        string json = JsonUtility.ToJson(dataObject);
        byte[] data = Encoding.UTF8.GetBytes(json);

        try
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
            udpClient.Send(data, data.Length, remoteEndPoint);
        }
        catch (SocketException socketException)
        {
            Debug.LogError("Socket exception: " + socketException.Message);
        }
    }

    void OnDestroy()
    {
        if (udpClient != null)
        {
            udpClient.Close(); // Close the UDP client when the script is destroyed
        }
    }
}
