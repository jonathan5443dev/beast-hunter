using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hero : MonoBehaviour {
    BoxCollider2D boxCollider;
    Rigidbody2D rigidBody;
    PowerUp powerUp;
    Animator anim;

    public float energy = 100;
    public Slider slider;
    public Text txt;

    bool walkingRight = true;
    float maxVel = 1.2f;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        anim = GetComponent<Animator> ();
        rigidBody.freezeRotation = true;
    }

    void Update () {
        if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("hero_death")) {
            if (energy <= 0) {
                energy = 0;
                anim.SetTrigger ("death");
            }
        } else {
            return;
        }
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
            SetAnimationState ();
        }
    }

    // Collision 
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.name.Equals ("shield")) {
            powerUp = other.gameObject.GetComponent<PowerUp> ();
        }
        if (other.gameObject.name.Equals ("skeleton")) {
            if (anim.GetCurrentAnimatorStateInfo (0).IsName ("hero_attack")) {
                Skeleton enemy = other.gameObject.GetComponent<Skeleton> ();
                if (enemy != null) enemy.ReceiveDamage ();
            }
        }
    }

    // Collision exit
    private void OnTriggerExit2D (Collider2D other) {
        powerUp = null;
    }

    // Change animation states
    void SetAnimationState () {
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

    public void ReceiveDamage () {
        energy -= 10;
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