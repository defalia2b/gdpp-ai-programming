using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _powerUpDuration;
    [SerializeField] private int _health;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Transform _respawnPoint;
    private Vector2 _moveVector;
    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive = false;


    public void Dead()
    {
        _health -= 1;
        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            SceneManager.LoadScene("LoseScreen");
        }

        UpdateUI();
    }

    public void PickPowerUp()
    {
        if (_powerUpCoroutine != null) StopCoroutine(_powerUpCoroutine);
        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        if (OnPowerUpStart != null) OnPowerUpStart();
        yield return new WaitForSeconds(_powerUpDuration);
        _isPowerUpActive = false;
        if (OnPowerUpStop != null) OnPowerUpStop();
    }

    private void Awake()
    {
        UpdateUI();
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
        var horizontal = _moveVector.x;
        // Vertical = s or down (-) & w or up (+)
        var vertical = _moveVector.y;

        var forward = _camera.transform.forward;
        var right = _camera.transform.right;
        forward.y = 0;
        right.y = 0;

        // Normalize agar kecepatan player tidak jadi lebih lambat jika tergantung arah kamera
        forward.Normalize();
        right.Normalize();

        // Normalize move agar kecepatan diagonal tidak lebih cepat dari jalan lurus
        var move = (forward * vertical + right * horizontal).normalized;
        _rigidbody.linearVelocity = move * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isPowerUpActive)
            if (other.gameObject.CompareTag("Enemy"))
                other.gameObject.GetComponent<Enemy>().Dead();
    }

    private void UpdateUI()
    {
        _healthText.text = "Health: " + _health;
    }
}