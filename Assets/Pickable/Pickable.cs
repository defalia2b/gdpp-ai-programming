using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] public Pickable PickableType;
    public Action<Pickable> OnPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPicked(this);
            Destroy(gameObject);
        }
    }
}
