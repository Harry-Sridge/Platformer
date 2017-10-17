using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{

    private Rigidbody2D rb2d;
    private float velocity;

    public int lifeTime;
    public float gravityMultipler = 0.5f; 

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddRelativeForce(Vector2.right * 100 * velocity);
    }

    private void Update()
    {
        rb2d.gravityScale *= gravityMultipler;
        Destroy(this.gameObject, lifeTime);
    }

    public void SetupProjectile(float velocity)
    {
        this.velocity = velocity;
    }
}
