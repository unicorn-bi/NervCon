using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDP;
using static Gtec.UnityInterface.BCIManager;
using System;
using Gtec.UnityInterface;

public class ClassificationScript : MonoBehaviour
{
    public Dictionary<int, string> idToControlCommand;
    //[SerializeField]
    //[Tooltip("The material to use when the target is selected")]
    //Material _correctSelectionMaterial;

    //[SerializeField]
    //[Tooltip("The material to use when the wrong target is selected")]
    //Material _wrongSelectionMaterial;

    public bool _update = false;

    private UdpController UdpController;
    public List<GameObject> selectedIds;
    public uint _selectedClass;
    public void Start()
    {
        //Debug.Log("dsadsa");
        BCIManager.Instance.ClassSelectionAvailable += OnClassSelectionAvailable;
        UdpController = FindObjectOfType<UdpController>();

        foreach (GameObject id in selectedIds)
        {
            id.SetActive(false);
        }

        idToControlCommand = new Dictionary<int, string>();
        idToControlCommand[0] = "a";
        idToControlCommand[1] = "b";
        idToControlCommand[2] = "x";
        idToControlCommand[3] = "y";
    }

    public void Update()
    {
        if (_update)
        {
            if((int)_selectedClass > 0 && (int)_selectedClass <= selectedIds.Count)
            {
                foreach (GameObject id in selectedIds)
                {
                    if (id.activeSelf)
                    {
                        id.SetActive(false);
                    }
                }
                selectedIds[(int)_selectedClass - 1].SetActive(true);
            }
            else
            {
                foreach (GameObject id in selectedIds)
                {
                    if (id.activeSelf)
                    {
                        id.SetActive(false);
                    }
                }
            }

            _update = false;
        }
    }

    private void OnClassSelectionAvailable(object sender, EventArgs e)
    {
        ClassSelectionAvailableEventArgs ea = (ClassSelectionAvailableEventArgs)e;
        _selectedClass = ea.Class;
        _update = true;
        Debug.Log(string.Format("Selected class: {0}", ea.Class));

        if ((int)_selectedClass != 0)
        {
            //Debug.Log(string.Format("Send command usp: {0}", idToControlCommand[(int)_selectedClass - 1]));

            UdpController.SendData(idToControlCommand[(int)_selectedClass-1]);
        }
    }

}
