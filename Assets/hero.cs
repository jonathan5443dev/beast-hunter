using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour {
    Rigidbody2D rigidBody;
    Animator anim;
    float maxVel = 1.2f;
    bool walkingRight = true;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    // Run every frame
    void FixedUpdate () {
        float v = Input.GetAxis ("Horizontal");
        Vector2 vel = new Vector2 (0, rigidBody.velocity.y);
        v *= maxVel;
        vel.x = v;
        rigidBody.velocity = vel;
        if (v != 0) {
            anim.SetTrigger ("walk");
        } else {
            anim.SetTrigger ("idle");

        }
        if (walkingRight && v < 0) {
            walkingRight = false;
            Flip ();
        } else if (!walkingRight && v > 0) {
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
}