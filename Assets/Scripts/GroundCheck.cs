using System;
using Player;
using UnityEngine;

namespace GroundCheck {
    public class GroundCheck : MonoBehaviour
    {
        const float groundCheckRadius = 0.2f;
        private bool _isGrounded;
        private LayerMask groundLayer;

        [SerializeField]
        private Transform _groundCheckCollider;
    }
}