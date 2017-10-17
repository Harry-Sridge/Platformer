using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	public float maxSpeed = 7;
	public float jumptakeOffSpeed = 7f;
	public float fallGravityMultiplier = 2f;

	public SpriteRenderer spriteRenderer;
    public WeaponTransformManager weaponTransformManager;

    bool isMoving = false;
    bool facingRight = false;
    bool isHeadingRight = true;
    bool isPointingRight = true;
    float weaponRotation;

	void Awake () {
	}

    protected override void ComputeVelocity ()
	{
        Vector2 move = Vector2.zero;
		move.x = Input.GetAxis ("Horizontal");

        isMoving = (move.x == 0) ? false : true;
        isHeadingRight = (move.x > 0) ? true : false;

		if (Input.GetButtonDown ("Jump") && grounded) 
		{
			velocity.y = jumptakeOffSpeed;		
		} 

		else if (Input.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0) 
			{
				velocity.y = velocity.y * 0.5f;
			}
		}

		//If the player is falling, apply extra gravity
		if (velocity.y < 0) 
		{
			velocity += Vector2.up * Physics2D.gravity.y * (fallGravityMultiplier - 1) * Time.deltaTime;
		}

		targetVelocity = move * maxSpeed;

        Debug.Log("Moving: " + isMoving + " Facing Right: " + facingRight + " Heading Right: " + isHeadingRight + " Pointing Right: " + isPointingRight);
        //Flip
        weaponRotation = weaponTransformManager.rotationZ;
        isPointingRight = (-90 <= weaponRotation && weaponRotation <= 90) ? true : false;

        //Not moving
        if (!isMoving)
        {
            //If gun is pointing right
            if (isPointingRight)
            {
                //and player is facing right
                if (facingRight)
                    return;

                else
                    Flip();
            }

            //if gun is pointing left
            else
            {
                //and player is facing right
                if (facingRight)
                    Flip();
                else
                    return;
            }
        }

        //Moving
        else
        {
            //if player is heading right
            if (isHeadingRight)
            {
                //and gun is pointing right
                if (isPointingRight)
                {
                    //and player is facing right
                    if (facingRight)
                        return;
                    else
                        Flip();
                }

                //and gun is pointing left
                else
                {
                    //and player is facing right
                    if (facingRight)
                        Flip();
                    else
                        return;
                }
            }

            //if player is heading left
            else
            {
                //and gun is pointing right
                if (isPointingRight)
                {
                    //and player is facing right
                    if (facingRight)
                        return;
                    else
                        Flip();
                }

                //and gun is pointing left
                else
                {
                    //and player is facing right
                    if (facingRight)
                        Flip();
                    else
                        return;
                }
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
