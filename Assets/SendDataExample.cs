using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SendDataExample : MonoBehaviour
{
    //private JsonUdpController jsonUdpController;
    private JsonStructure data;
    private bool send = false;
    void Start()
    {
        //jsonUdpController = FindObjectOfType<JsonUdpController>(); // Find the JsonUdpController in the scene

        data = new JsonStructure
        {
            Key = "exampleValue",
            classId = 1,
            time = 10
        };

        //jsonUdpController.SendData(data);
    }

    public void Update()
    {
        if (!send)
        {
            Debug.Log("Send USD");
            //jsonUdpController.SendData(data);
            send = true;
        }
    }
}
