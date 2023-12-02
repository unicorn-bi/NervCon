using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script generates a randomized sequence of objects to be spawned at specified positions.
/// </summary>
public class RandSequenceGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The list spawn positions for the sequence")]
    private List<GameObject> SpawnPositions = new List<GameObject>();

    [SerializeField]
    [Tooltip("The list of objects to spawn")]
    private List<GameObject> SpawnTargets = new List<GameObject>();

    private List<GameObject> _sequence;

    private int _countSuccess = 0;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        _sequence = new List<GameObject>();
        // Ensure both lists have the necessary elements
        if (SpawnPositions.Count < 8 || SpawnTargets.Count < 2)
        {
            Debug.LogError("Not enough spawn positions or prefabs.");
            return;
        }

    }


    /// <summary>
    /// Generates a randomized sequence of objects to be spawned at the specified positions.
    /// </summary>
    public void GenerateSequence()
    {
        ClearSequence();
        // Determine the number of objects to spawn for each prefab
        int objectsPerPrefab = Mathf.CeilToInt(SpawnPositions.Count / SpawnTargets.Count);

        for(int i=0; i<SpawnTargets.Count; i++)
        {   
            GameObject currentTarget = SpawnTargets[i];
            for(int j=0; j<objectsPerPrefab; j++)
            {
                GameObject newTarget = Instantiate(currentTarget);
                _sequence.Add(newTarget);
            }
        }

        // Shuffle the sequence
        Shuffle(_sequence);

        for(int i = 0; i < SpawnPositions.Count; i++)
        {
            _sequence[i].transform.parent = SpawnPositions[i].transform;

            // Optionally, reset the local position, rotation, and scale of the childObject
            _sequence[i].transform.localPosition = Vector3.zero;
            _sequence[i].transform.localRotation = Quaternion.identity;
            _sequence[i].transform.localScale = SpawnPositions[i].transform.localScale;
        }
    }

    public void SelectNext(int classID)
    {
        if(classID == 0)
        {
            classID = Random.Range(1, SpawnTargets.Count + 1);
            Debug.LogWarning("Selected in Debug Mode! Generating random Class ID: " + classID);
        }

        if(_sequence == null || _sequence.Count == 0)
        {
            Debug.LogError("No sequence generated.");
            return;
        } 

        List<TaskTarget> taskTargets = new List<TaskTarget>();
        foreach(GameObject obj in _sequence)
        {
            taskTargets.Add(obj.GetComponent<TaskTarget>());
        }
        // Iterate through the sequence and select objects from left to right
        foreach(TaskTarget target in taskTargets)
        {
            if(!target._isSelected)
            {
                target.ActivateTarget(classID);
                if(classID == target._classID)
                {
                    _countSuccess++;
                    Debug.Log("Correct! Remaining: " + (_sequence.Count - _countSuccess));
                } else
                {
                    Debug.Log("Wrong! Remaining: " + (_sequence.Count - _countSuccess));
                }
                break;
            }
        }
    }

    /// <summary>
    /// Clears the current sequence.
    /// </summary>
    public void ClearSequence()
    {
        if(_sequence != null && _sequence.Count > 0)
        {
            // Iterate through the sequence and despawn each object
            foreach (GameObject obj in _sequence)
            {
                // Use either Destroy or SetActive(false) based on your needs
                Destroy(obj); // Uncomment this line to destroy the objects
                
            }
            // Clear the sequence list
            _sequence.Clear();
            _sequence = new List<GameObject>();
            _countSuccess = 0;
        }
        
    }


    /// <summary>
    /// Shuffles the elements in the given list using the Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="sequence">The list to be shuffled.</param>
    void Shuffle<T>(List<T> sequence)
    {
        int n = sequence.Count;
        System.Random rng = new System.Random();

        // Perform Fisher-Yates shuffle
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = sequence[k];
            sequence[k] = sequence[n];
            sequence[n] = value;
        }
    }
}
