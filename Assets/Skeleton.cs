using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour {
    Rigidbody2D rigidBody;
    Animator anim;

    public float speed = -1f;
    public Slider slider;
    public Text txt;
    public Text defeatedTxt;

    public float energy = 100;
    bool attackingMode = false;
    bool isDeath = false;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        rigidBody.freezeRotation = true;

    }

    void Update () {
        if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("skeleton_death")) {
            CheckDeath ();
        } else {
            return;
        }
        slider.value = energy;
        txt.text = energy.ToString ();
    }

    // Run every frame
    void FixedUpdate () {
        SetAnimationState ();
    }

    // Trigger limits collision
    void OnTriggerEnter2D (Collider2D other) {
        Flip ();
        if (other.gameObject.name.Equals ("hero")) {
            if (anim.GetCurrentAnimatorStateInfo (0).IsName ("skeleton_atack")) {
                hero hero = other.gameObject.GetComponent<hero> ();
                if (hero != null) hero.ReceiveDamage ();
            }
        }
    }

    public void ReceiveDamage () {
        energy -= 10;
    }

    // AttackMode
    public void AttackMode (bool selection) {
        attackingMode = selection;
    }

    // Change x direction
    void Flip () {
        speed *= -1;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Death
    void CheckDeath () {
        if (energy <= 0) {
            energy = 0;
            anim.SetTrigger ("death");
            isDeath = true;
            Destroy (gameObject, 1.5f);
            defeatedTxt.text = "Eliminado!!!";
        }
    }

    // Change animation states
    void SetAnimationState () {
        if (!isDeath) {
            if (attackingMode) {
                var scale = transform.localScale;
                if (scale.x < 1) {
                    attackingMode = true;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                anim.SetTrigger ("attack");
            } else {
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
    }
}