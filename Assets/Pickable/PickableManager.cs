using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ScoreManager _scoreManager;
    private List<Pickable> _pickableList = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        var pickableObjects = FindObjectsByType<Pickable>(FindObjectsSortMode.None);
        for (var i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }

        _scoreManager.SetMaxScore(_pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (_scoreManager != null) _scoreManager.AddScore(1);

        if (pickable.PickableType == PickableTypes.PowerUp) _player?.PickPowerUp();
        if (_pickableList.Count <= 0) SceneManager.LoadScene("WinScreen");
    }
}