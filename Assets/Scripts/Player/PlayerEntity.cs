using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Tools;
using Core.Enums;

namespace Player
{
    [RequireComponent(typeof (Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;

        [SerializeField] private Direction _direction;

        [Header("HorizontalMovement")]
        [SerializeField] private float _jumpForce;

        const float groundCheckRadius = 0.2f;

        private bool _isJumping;

        [SerializeField] public bool _isGrounded;

        private Rigidbody2D _rigidbody;
        private Animator animator;

        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private Transform _groundCheckCollider;

        [SerializeField] private DirectionalCamerasPair _cameras;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        public void MoveHorizontally(float direction)
        {
            SetDirection (direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
            UnityEngine.Debug.Log(_rigidbody.velocity);

            int movingValue = 0;

            if (Mathf.Ceil(Mathf.Abs(_rigidbody.velocity.x)) > 0) {
                movingValue = 1;
            }
            animator.SetFloat("xVelocity", movingValue);
        }

        public void Jump()
        {
            Debug.Log(Vector2.up * _jumpForce);
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }

        public void GroundCheck()
        {
            _isGrounded = false;
            Collider2D[] colliders =
                Physics2D
                    .OverlapCircleAll(_groundCheckCollider.position,
                    groundCheckRadius,
                    groundLayer);
            if (colliders.Length > 0)
            {
                _isGrounded = true;
            }
        }

        private void SetDirection(float direction)
        {
            if ((_direction == Direction.Right && direction < 0) || (_direction == Direction.Left && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;

            foreach (var cameraPair in _cameras.DirectionalCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _direction;
            }
        }
    }
}
