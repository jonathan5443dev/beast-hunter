using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed = -1f;
    Rigidbody2D rigidBody;
    
    // Constructor
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D> ();
    }

    // Run every frame
    void FixedUpdate()
    {
        Vector2 velocityVector = new Vector2 (speed, 0);   
        rigidBody.velocity = velocityVector;  
    }

    // Trigger limits collision
    void OnTriggerEnter2D(Collider2D col)
    {
        Flip();
    }
    
    // Change x direction
    void Flip() {
        speed *= -1;
        var scale =  transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
