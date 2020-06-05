using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hero : MonoBehaviour {
    Rigidbody2D rigidBody;
    Animator anim;
    float maxVel = 1.2f;
    bool walkingRight = true;

    public Slider slider;
    public Text txt;

    public float energy = 100;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    void Update () {
        if (System.Math.Abs (Input.GetAxis ("Fire2")) > 0.1f) {
            anim.SetTrigger ("attack");
        }
        slider.value = energy;
        txt.text = energy.ToString ();
    }

    // Run every frame
    void FixedUpdate () {
        float v = Input.GetAxis ("Horizontal");
        Vector2 vel = new Vector2 (0, rigidBody.velocity.y);
        v *= maxVel;
        vel.x = v;
        rigidBody.velocity = vel;
        if (v != 0) {
            anim.ResetTrigger ("idle");
            anim.SetTrigger ("walk");
        } else {
            anim.ResetTrigger ("walk");
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