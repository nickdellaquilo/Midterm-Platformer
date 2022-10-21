using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public Transform feetTrans;
    public LayerMask groundLayer;
    [SerializeField] public int speed = 5;
    [SerializeField] public int accel = 1;
    [SerializeField] public int runMult = 2;
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public int jumpForce = 2500;
    public int numJumps = 0;
    //[SerializeField] int coyoteTime = 8;
    public bool grounded = false;
    private float slideSpeed = 8f;
    private float xSpeed = 0;
    private float ySpeed = 0;
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
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        ySpeed = _rigidbody.velocity.y;
        _animator.SetFloat("xSpeed", Mathf.Abs(xSpeed));
        _animator.SetFloat("ySpeed", Mathf.Abs(ySpeed));
        
        if (Input.GetButton("Run"))
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
        if (Input.GetButtonDown("Slide"))
        {
            Debug.Log("Print Slide");
            _animator.SetTrigger("Slide");
            if (xSpeed > 0) 
            
            {
                _rigidbody.AddForce(Vector2.right * slideSpeed);
                
            }
            else
            {
                _rigidbody.AddForce(Vector2.left * slideSpeed);
            }
            //StartCoroutine("SlideEnd");
        }
        if (xSpeed < 3)
        {
            _animator.ResetTrigger("Slide");
        }
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
