using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDP;
using static Gtec.UnityInterface.BCIManager;
using System;
using Gtec.UnityInterface;

public class ClassificationScript : MonoBehaviour
{
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
    //public void ActivateTarget(int selectedClass)
    //{
    //    Debug.Log("Called");
    //    UdpController.SendData(selectedClass);

    //    if (!_isSelected)
    //    {
    //        UdpController.SendData(selectedClass);
    //        if (selectedClass == _classID)
    //        {
    //            GetComponent<Renderer>().material = _correctSelectionMaterial;
    //            _isSelected = true;
    //        }
    //        else
    //        {
    //            GetComponent<Renderer>().material = _wrongSelectionMaterial;
    //        }
    //    }
    //}

    private void OnClassSelectionAvailable(object sender, EventArgs e)
    {
        ClassSelectionAvailableEventArgs ea = (ClassSelectionAvailableEventArgs)e;
        _selectedClass = ea.Class;
        _update = true;
        Debug.Log(string.Format("Selected class: {0}", ea.Class));

        if ((int)_selectedClass != 0)
        {
            UdpController.SendData((int)_selectedClass);
        }
    }

}
