using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private void Start()
    {
        InputAction moveAction = InputSystem.actions.FindAction("move");
    }
    
    private void Update()
    {
    }
}
