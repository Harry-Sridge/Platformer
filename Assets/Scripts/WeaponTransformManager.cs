using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTransformManager : MonoBehaviour {

    public int rotationOffset = 0;
    public float rotationZ;

    void Update()
    {
        Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        offset.Normalize();

        rotationZ = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
    }
}
