using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {
    public float speed = -1f;
    Rigidbody2D rigidBody;
    Animator anim;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    // Run every frame
    void FixedUpdate () {
        setAnimationState ();
    }

    // Trigger limits collision
    void OnTriggerEnter2D (Collider2D col) {
        Flip ();
    }

    // Change x direction
    void Flip () {
        speed *= -1;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Change animations states
    void setAnimationState () {
        bool isWalking = anim.GetCurrentAnimatorStateInfo (0).IsName ("skeleton_walking");
        bool shouldIdle = Random.value < 1f / (60f * 3f);
        if (isWalking) {
            if (shouldIdle) {
                anim.SetTrigger ("idle");
            } else {
                Vector2 velocityVector = new Vector2 (speed, 0);
                rigidBody.velocity = velocityVector;
            }
        } else {
            bool shouldWalk = Random.value < 1f / (60f * 3f);
            bool shouldAttack = Random.value < 1f / (60f * 3f);
            if (shouldWalk) {
                anim.SetTrigger ("walk");
            }
            if (shouldAttack) {
                anim.SetTrigger ("attack");
            }
        }
    }

}