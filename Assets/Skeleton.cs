using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed = -1f;
    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate()
    {
        Vector2 velocityVector = new Vector2 (speed, 0);   
        rigidBody.velocity = velocityVector;  
    }
}
