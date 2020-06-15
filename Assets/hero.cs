using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hero : MonoBehaviour {
    Animator anim;
    public Text txt;
    float maxVel = 1.2f;
    public Slider slider;
    Rigidbody2D rigidBody;
    bool walkingRight = true;
    public float energy = 100;
    BoxCollider2D boxCollider;
    PowerUp powerUp;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        anim = GetComponent<Animator> ();
        rigidBody.freezeRotation = true;
    }

    void Update () {
        if (System.Math.Abs (Input.GetAxis ("Fire2")) > 0.1f) {
            anim.SetTrigger ("attack");
        } else {
            anim.ResetTrigger ("attack");
        }
        if (powerUp != null) {
            powerUp.Grab ();
        }
        slider.value = energy;
        txt.text = energy.ToString ();
    }

    // Run every frame
    void FixedUpdate () {
        if (isGrounded ()) {
            setAnimationState ();
        }
    }

    // Collision 
    private void OnTriggerStay2D (Collider2D collider) {
        if (collider.gameObject.name.Equals ("shield")) {
            powerUp = collider.gameObject.GetComponent<PowerUp> ();
        }
    }

    // Collision exit
    private void OnTriggerExit2D (Collider2D other) {
        powerUp = null;
    }

    // Change animation states
    void setAnimationState () {
        float horizontalMovement = Input.GetAxis ("Horizontal");
        float verticalMovement = Input.GetAxis ("Vertical");
        Vector2 vel = new Vector2 (0, 0);
        if (horizontalMovement != 0 && verticalMovement > 0) {
            horizontalMovement *= maxVel;
            vel.x = horizontalMovement;
            vel.y = 4;
            anim.SetTrigger ("jump");
        }
        if (horizontalMovement != 0) {
            horizontalMovement *= maxVel;
            vel.x = horizontalMovement;
            rigidBody.velocity = vel;
            anim.ResetTrigger ("idle");
            anim.SetTrigger ("walk");
        } else {
            if (verticalMovement > 0) {
                vel.y = 4;
                rigidBody.velocity = vel;
                anim.SetTrigger ("jump");
            } else {
                anim.ResetTrigger ("walk");
                anim.SetTrigger ("idle");
            }
        }
        if (walkingRight && horizontalMovement < 0) {
            walkingRight = false;
            Flip ();
        } else if (!walkingRight && horizontalMovement > 0) {
            walkingRight = true;
            Flip ();
        }
    }

    // Change x direction
    void Flip () {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    // Check if the hero is grounded
    bool isGrounded () {
        return transform.Find ("GroundCheck").GetComponent<GroundCheck> ().isGrounded;
    }
}