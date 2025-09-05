using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    private Vector2 _moveVector;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void FixedUpdate()
    {
        // Horizontal = a or left (-) & d or right (+)
        float horizontal = _moveVector.x;
        // Vertical = s or down (-) & w or up (+)
        float vertical = _moveVector.y;
        
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;
        forward.y = 0;
        right.y = 0;
        
        // Normalize agar kecepatan player tidak jadi lebih lambat jika tergantung arah kamera
        forward.Normalize();
        right.Normalize();
        
        // Normalize move agar kecepatan diagonal tidak lebih cepat dari jalan lurus
        Vector3 move = (forward * vertical + right * horizontal).normalized;
        _rigidbody.linearVelocity = move * _speed;
    }
}