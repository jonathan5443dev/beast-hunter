using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour {
    public float speed = -1f;
    Rigidbody2D rigidBody;
    Animator anim;

    public Slider slider;
    public Text txt;

    public float energy = 100;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        rigidBody.freezeRotation = true;
    }

    void Update () {
        slider.value = energy;
        txt.text = energy.ToString ();
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

    // Change animation states
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