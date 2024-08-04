using System;
using UnityEngine;
using static Utils;

public class TurretController : MonoBehaviour {
    [SerializeField] private float turretRotationSpeed = 60f;
    [SerializeField] private Transform child;
    
    private Camera cam;
    
    private float currentRotation;
    
    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        Vector2 mouse_direction = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
        mouse_direction.Normalize();
        
        var target_rotation = Rem(
            (Mathf.Atan2(mouse_direction.y, mouse_direction.x) * Mathf.Rad2Deg) + 90f - transform.rotation.eulerAngles.z, 
            360f
        );

        var rotation_delta = GetRotationDelta(currentRotation, target_rotation);

        currentRotation += Mathf.Clamp(rotation_delta, -turretRotationSpeed, turretRotationSpeed) * Time.deltaTime;
        child.localRotation = Quaternion.Euler(0, 0, currentRotation);
    }

    private float GetRotationDelta(float current, float target) {
        float positive;
        float negative;
        if (target > current) {
            positive = target - current;
            negative = current + (360 - target);
        }
        else {
            positive = target + (360 - current);
            negative = current - target;
        }

        return positive < negative ? positive : -negative;
    }
}