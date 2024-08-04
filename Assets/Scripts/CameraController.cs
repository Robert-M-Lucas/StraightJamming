using System;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform follow;
    [SerializeField] private float followSpeed = 5f;

    public void Update() {
        var delta = (Vector2) follow.position - (Vector2) transform.position;
        transform.position += (Vector3) delta * Mathf.Min(Time.deltaTime * followSpeed, 1f);
    }
}