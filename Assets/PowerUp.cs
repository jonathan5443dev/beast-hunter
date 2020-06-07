using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    Animator anim;
    bool isVisible = true;

    // Start is called before the first frame update
    void Start () {
        anim = GetComponent<Animator> ();

    }

    // Update is called once per frame
    void Update () {

    }

    public bool Grab () {
        if (isVisible) {
            anim.SetTrigger ("grab");
            isVisible = false;
        }
        return isVisible;
    }
}