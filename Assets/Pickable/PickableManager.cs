using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = FindObjectsByType<Pickable>(FindObjectsSortMode.None);
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
        Debug.Log(_pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
