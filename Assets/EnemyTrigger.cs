using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {
    // Start is called before the first frame update
    Collider2D colliderEnem = null;
    public int delay = 1;
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.name.Equals ("skeleton") && colliderEnem == null) {
            colliderEnem = other;
            Invoke ("ActivateAttackMode", delay);
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other == colliderEnem) {
            Invoke ("DisableAttackMode", delay);
        }
    }

    void ActivateAttackMode () {
        Debug.Log ("prender ataque");
        colliderEnem.gameObject.GetComponent<Skeleton> ().AttackMode (true);
    }

    void DisableAttackMode () {
        Debug.Log ("apagar ataque");
        colliderEnem.gameObject.GetComponent<Skeleton> ().AttackMode (false);
    }

}