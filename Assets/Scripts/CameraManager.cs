using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 newPosition = target.position + offset;
        Vector3 newSmoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        transform.position = newSmoothedPosition;
    }
}
