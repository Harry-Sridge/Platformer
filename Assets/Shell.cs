using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    Rigidbody2D rb2d;
    public float force = 10f;
    public int lifetime = 3;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddRelativeForce(transform.up * 10 * force);
    }

    private void Update()
    {
        Destroy(this.gameObject, lifetime);
    }

}
