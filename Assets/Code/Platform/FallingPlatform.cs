using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            Invoke ("Drop", 0.5f);
            Destroy(gameObject, 1f);
        }
    }

    private void Drop(){
        rb.isKinematic = false;
    }

    
    
}
