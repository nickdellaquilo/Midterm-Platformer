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

    private Animator anim;
    private BoxCollider2D boxCollider;

    public Rigidbody2D rb;

    private float lifetime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void Start()
    {
        rb.velocity = transform.right * speed * Time.deltaTime * transform.localScale.x;
    }


    private void Update()
    {
        if (hit) return;
 
        transform.Translate(transform.right * speed * Time.deltaTime * transform.localScale.x);
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("shoot");

        
    }

    public void SetDirection(float _direction) 
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        lifetime = 0;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) 
            localScaleX = -localScaleX;
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate() 
    {
        gameObject.SetActive(false);
    }


}
