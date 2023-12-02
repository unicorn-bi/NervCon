using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTarget : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The class ID of the target")]
    public int _classID;

    [SerializeField]
    [Tooltip("The material to use when the target is selected")]
    Material _correctSelectionMaterial;

    [SerializeField]
    [Tooltip("The material to use when the wrong target is selected")]
    Material _wrongSelectionMaterial;

    public bool _isSelected = false;


    public void ActivateTarget(int selectedClass)
    {
        if(!_isSelected)
        {
            if (selectedClass == _classID)
            {
                GetComponent<Renderer>().material = _correctSelectionMaterial;
                _isSelected = true;
            }
            else
            {
                GetComponent<Renderer>().material = _wrongSelectionMaterial;
            }
        }
    }   
}
