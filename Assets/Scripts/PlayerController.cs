
using System;
using Unity.VisualScripting;
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
        
        var move_delta = ((Vector2) transform.up * (forwardInput * movementSpeed * Time.fixedDeltaTime))
                         + persistentVelocity;

        var distance = move_delta.magnitude;
        if (distance > 0f) {
            var direction = move_delta.normalized;
            var hit = Physics2D.CircleCast(transform.position, radius, direction,
                distance, collisionLayers);
            if (hit) {
                var delta = hit.centroid - rb.position;
                var new_position = delta.normalized * (delta.magnitude * 0.999f);
                var remaining = distance - (new_position - rb.position).magnitude;

                var new_direction = (direction * (Vector2.one - hit.normal.Abs())).normalized;
                
                var hit2 = Physics2D.CircleCast(new_position, radius, new_direction,
                    remaining, collisionLayers);

                if (hit2) {
                    rb.position = hit2.centroid;
                }
                else {
                    rb.position = new_position + (new_direction * remaining);
                }
            }
            else {
                rb.position += move_delta;
            }
        }


        rb.rotation += rotationInput * rotationSpeed * Time.fixedDeltaTime;
    }
    
    private void OnCollisionStay2D (Collision2D other) {
        if (!ContainsLayer(collisionLayers, other.gameObject.layer)) return;
        
        var contact_point = other.GetContact(0);
        rb.position = contact_point.point + contact_point.normal * (radius + 0.00001f);

        persistentVelocity *= (Vector2.one - contact_point.normal.Abs()).normalized;
    }
}