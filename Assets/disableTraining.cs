using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableTraining : MonoBehaviour
{
    public GameObject trainingObject;

    // Start is called before the first frame update
    public void OnDisableObject()
    {
        Debug.Log("Disable Training Object");
        trainingObject.SetActive(false);
    }
}
