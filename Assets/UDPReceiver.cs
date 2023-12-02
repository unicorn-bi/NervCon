using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    public string remoteIP = "127.0.0.1";
    public int listenPort = 12345;
    private Thread receiveThread;
    private UdpClient client;
    private bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(listenPort);
        while (isRunning)
        {
            try
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), listenPort);

                byte[] data = client.Receive(ref remoteEndPoint);

                string text = Encoding.UTF8.GetString(data);

                // Process received data here
                Debug.Log("Received: " + text);
                // Optionally deserialize the JSON string back into an object
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }

    void OnDestroy()
    {
        isRunning = false;
        if (client != null)
        {
            client.Close();
        }
        if (receiveThread != null)
        {
            receiveThread.Abort();
        }
    }
}
