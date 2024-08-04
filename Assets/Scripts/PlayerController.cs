using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void JumpHandler(InputAction.CallbackContext context) {
        rb.AddForce(Vector2.up * 100);
    }
}
