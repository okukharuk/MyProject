using System.Diagnostics;
using System;
using Player;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;

    private float _direction;

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate() {
        _playerEntity.GroundCheck();
        if (Input.GetButtonDown("Jump") && _playerEntity._isGrounded) {
            _playerEntity.Jump();
        }
         if (_playerEntity._mobilePlatform)
                _playerEntity.JoystickMovement(_direction);
            else
                _playerEntity.MoveHorizontally(_direction);
    }
}

