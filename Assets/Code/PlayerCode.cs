using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public Transform feetTrans;
    public LayerMask groundLayer;
    [SerializeField] public float speed = 7.5f;
    [SerializeField] public float accel = 1.0f;
    [SerializeField] public float runMult = 2.0f;
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public int jumpForce = 2500;
    public int numJumps = 0;
    //[SerializeField] int coyoteTime = 8;
    public bool grounded = false;
    private float slideSpeed = 25.0f;
    private float xSpeed = 0.0f;
    private float ySpeed = 0.0f;
    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _renderer;
    public Transform fireLoc;
    Vector2 spawnPoint;
    bool isDead = false;
    bool sliding = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feetTrans.position, 0.9f, groundLayer);
        _animator.SetBool("Grounded", grounded);
        //if (grounded) { numJumps = maxJumps; }
        ySpeed = _rigidbody.velocity.y;
        _animator.SetFloat("xSpeed", Mathf.Abs(xSpeed));
        _animator.SetFloat("ySpeed", Mathf.Abs(ySpeed));
        _animator.SetBool("Slide", sliding);
        
        if (!sliding) { xSpeed = Input.GetAxisRaw("Horizontal") * speed; }
        else { xSpeed *= 0.999f; }

        if (Input.GetButton("Run") && !sliding)
        {
            xSpeed *= runMult;
            _animator.SetBool("Shoot", true);
        }
        else { _animator.SetBool("Shoot", false); }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _animator.SetBool("Jump", true);
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce));
        }
        if (Input.GetButtonDown("Fire1"))
        {  
            _animator.SetTrigger("Shoot");
        }
        if (grounded)
        {
            _animator.SetBool("Jump", false);
            
        }
        if (Input.GetButtonDown("Slide") && !sliding && Mathf.Abs(xSpeed) >= speed)
        {
            Debug.Log("Print Slide");
            //_animator.SetTrigger("Slide");

            //xSpeed *= 3;

            
            if (xSpeed > 0) 
            {
                //_rigidbody.AddForce(Vector2.right * slideSpeed);
                xSpeed = slideSpeed;
            }
            else
            {
                //_rigidbody.AddForce(Vector2.left * slideSpeed);
                xSpeed = -slideSpeed;
            }
            

            //StartCoroutine("SlideEnd");
            sliding = true;
        }
        if (Mathf.Abs(xSpeed) <= speed)
        {
            //_animator.ResetTrigger("Slide");
            sliding = false;
        }
        /*
        if (sliding)
        {
            
        }
        */
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(xSpeed, ySpeed);

        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2 (-1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {

        }
        if (other.tag == "Enemy"){
            other.GetComponent<Health>().TakeDamage(1);
        }
    }

    IEnumerator PlayerRespawn(Transform other)
    {
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        isDead = false;
        spawnPoint = other.transform.position;
    }

    /*
    IEnumerator SlideEnd() {
        yield return new WaitForSeconds(0.5f);
        _animator.Play("Idle");
    }
    */

}
