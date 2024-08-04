
using System;
using UnityEngine;
using static Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float rotationSpeed = 60f;
    
    private Rigidbody2D rb;

    private float forwardInput;
    private float rotationInput;
    private Vector2 persistentVelocity;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void InputUpdate() {
        var forward_input = 0f;
        if (Input.GetKey(KeyCode.W)) forward_input += 1f;
        if (Input.GetKey(KeyCode.S)) forward_input -= 1f;
        var rotation_input = 0f;
        if (Input.GetKey(KeyCode.A)) rotation_input += 1f;
        if (Input.GetKey(KeyCode.D)) rotation_input -= 1f;

        forwardInput = forward_input;
        rotationInput = rotation_input;
    }
    
    private void FixedUpdate() {
        InputUpdate();
        
        rb.position += ((Vector2) transform.up * (forwardInput * movementSpeed * Time.fixedDeltaTime)) 
                            + persistentVelocity;

        rb.rotation += rotationInput * rotationSpeed * Time.fixedDeltaTime;
    }
    
    private void OnCollisionStay2D (Collision2D other) {
        if (!ContainsLayer(collisionLayers, other.gameObject.layer)) return;
        
        var contact_point = other.GetContact(0);
        rb.position = contact_point.point + contact_point.normal * (radius + 0.0001f);

        persistentVelocity *= Vector2.one - contact_point.normal;
    }
}