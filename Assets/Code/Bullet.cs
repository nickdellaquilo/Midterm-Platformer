using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private bool hit;
    private float direction;

    //private Animator anim;
    private BoxCollider2D boxCollider;

    public Rigidbody2D rb;

    private float lifetime;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        hit = true;
        if (collision.tag == "Enemy") {
            Destroy(collision.gameObject);
        }
        
        Destroy(gameObject);

        
    }

    


}
